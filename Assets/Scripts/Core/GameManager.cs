using System.Collections.Generic;
using System.Linq;
using PixelCross.Data;
using PixelCross.Economy;
using PixelCross.Gacha;
using PixelCross.Inventory;
using PixelCross.Match;
using PixelCross.SaveLoad;
using PixelCross.Scouting;
using PixelCross.Tutorial;
using UnityEngine;
using DateTime = System.DateTime;
using Random = System.Random;
using Math = System.Math;

namespace PixelCross.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public TurnManager Turns { get; private set; } = new TurnManager();
        public TeamData PlayerTeam { get; private set; }
        public List<RivalSchoolData> RivalSchools { get; private set; } = new List<RivalSchoolData>();
        public List<ScheduledMatch> CurrentYearSchedule { get; private set; } = new List<ScheduledMatch>();
        public TutorialStep TutorialProgress { get; set; } = TutorialStep.NotStarted;

        private readonly Random _rng = new Random();
        private DateTime _lastLoginDateUtc = DateTime.MinValue;
        private int _consecutiveLoginDays;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void BindTurnEvents()
        {
            Turns.OnNewYearStarted += HandleNewYearStarted;
            Turns.OnIntercollegiateFinished += HandleIntercollegiateFinished;
        }

        public void StartNewGame(string universityName, string managerName)
        {
            PlayerTeam = new TeamData(universityName, managerName)
            {
                ScoutTickets = ScoutSystem.DefaultTicketsPerYear,
                Reputation = 10,
                NationalRanking = 20
            };
            PlayerTeam.Roster = RosterGenerator.GenerateInitialRoster(currentYear: 1, _rng);

            RivalSchools = RivalSchoolDatabase.CreateDefaultSchools();
            Turns = new TurnManager();
            BindTurnEvents();
            GenerateSeasonSchedule();

            TutorialProgress = TutorialStep.NotStarted;
            _lastLoginDateUtc = DateTime.MinValue;
            _consecutiveLoginDays = 0;
        }

        public LoginBonusSystem.LoginResult ProcessDailyLogin() =>
            LoginBonusSystem.ProcessLogin(PlayerTeam, ref _lastLoginDateUtc, ref _consecutiveLoginDays, DateTime.UtcNow);

        private void HandleNewYearStarted(int year)
        {
            RetirementSystem.AdvanceGradesForNewYear(PlayerTeam);
            PlayerTeam.Roster.AddRange(RosterGenerator.GenerateFreshmenIntake(year, _rng));
            PlayerTeam.ScoutTickets = ScoutSystem.DefaultTicketsPerYear;
            GenerateSeasonSchedule();
        }

        private void HandleIntercollegiateFinished(int year)
        {
            RetirementSystem.ProcessGraduation(PlayerTeam, _rng);
        }

        private void GenerateSeasonSchedule()
        {
            var schedule = LeagueScheduleGenerator.GenerateLeagueSchedule(RivalSchools, _rng);

            var intercollegiateWeeks = TurnManager.GetWeeksInYearForPhase(SeasonPhase.Intercollegiate);
            if (intercollegiateWeeks.Count > 0)
            {
                schedule.Add(new ScheduledMatch(intercollegiateWeeks[0], opponent: null, isIntercollegiate: true));
            }

            CurrentYearSchedule = schedule.OrderBy(m => m.Week).ToList();
        }

        public ScheduledMatch GetMatchThisWeek() =>
            CurrentYearSchedule.FirstOrDefault(m => m.Week == Turns.CurrentWeek);

        public MatchOutcome PlayScheduledMatch()
        {
            var match = GetMatchThisWeek();
            if (match == null) return null;

            var outcome = new MatchOutcome { IsIntercollegiate = match.IsIntercollegiate, Opponent = match.Opponent };

            if (match.IsIntercollegiate)
            {
                var tournamentResult = IntercollegiateSystem.RunTournament(PlayerTeam, RivalSchools, _rng);
                outcome.TournamentResult = tournamentResult;
                ApplyIntercollegiateResult(tournamentResult);
            }
            else
            {
                var result = MatchSimulator.SimulateAgainstRival(PlayerTeam, match.Opponent, _rng);
                outcome.Result = result;
                ApplyLeagueResult(result);
            }

            CurrentYearSchedule.Remove(match);
            AdvanceWeek();
            return outcome;
        }

        private void ApplyLeagueResult(MatchResult result)
        {
            // Placeholder reputation/ranking swing; needs real balancing later.
            if (result.HomeWon) PlayerTeam.Reputation += 5;
            else if (!result.IsDraw) PlayerTeam.Reputation = Math.Max(0, PlayerTeam.Reputation - 2);

            PlayerTeam.NationalRanking = Math.Clamp(
                PlayerTeam.NationalRanking - (result.HomeWon ? 1 : 0),
                1, RivalSchools.Count + 1);
        }

        private void ApplyIntercollegiateResult(IntercollegiateSystem.TournamentResult result)
        {
            // Placeholder scoring; needs real balancing later.
            PlayerTeam.Reputation += result.RoundsWon * 10;
            if (result.PlayerIsChampion)
            {
                PlayerTeam.Reputation += 50;
                PlayerTeam.NationalRanking = 1;
            }
        }

        public void AdvanceWeek()
        {
            Turns.AdvanceWeek();
        }

        public bool TryGachaPullSingleWithTicket(out Item item)
        {
            var success = GachaSystem.TryPullSingleWithTicket(PlayerTeam, _rng, out item);
            if (success) InventorySystem.AddItem(PlayerTeam, item);
            return success;
        }

        public bool TryGachaPullTenWithTicket(out List<Item> items)
        {
            var success = GachaSystem.TryPullTenWithTicket(PlayerTeam, _rng, out items);
            if (success) InventorySystem.AddItems(PlayerTeam, items);
            return success;
        }

        public bool TryGachaPullSingleWithPremiumCurrency(out Item item)
        {
            var success = GachaSystem.TryPullSingleWithPremiumCurrency(PlayerTeam, _rng, out item);
            if (success) InventorySystem.AddItem(PlayerTeam, item);
            return success;
        }

        public bool TryGachaPullTenWithPremiumCurrency(out List<Item> items)
        {
            var success = GachaSystem.TryPullTenWithPremiumCurrency(PlayerTeam, _rng, out items);
            if (success) InventorySystem.AddItems(PlayerTeam, items);
            return success;
        }

        public bool UseInventoryItem(PlayerData player, int inventoryIndex) =>
            InventorySystem.UseItem(PlayerTeam, player, inventoryIndex);

        public void SaveGame(string slot = "default")
        {
            var data = new GameSaveData
            {
                CurrentYear = Turns.CurrentYear,
                CurrentWeek = Turns.CurrentWeek,
                PlayerTeam = PlayerTeam,
                RivalSchools = RivalSchools,
                CurrentYearSchedule = CurrentYearSchedule,
                TutorialProgress = TutorialProgress,
                LastLoginDateUtc = _lastLoginDateUtc.ToString("o"),
                ConsecutiveLoginDays = _consecutiveLoginDays
            };
            SaveSystem.Save(data, slot);
        }

        public bool LoadGame(string slot = "default")
        {
            if (!SaveSystem.TryLoad(slot, out var data)) return false;

            PlayerTeam = data.PlayerTeam;
            RivalSchools = data.RivalSchools;
            CurrentYearSchedule = data.CurrentYearSchedule ?? new List<ScheduledMatch>();
            TutorialProgress = data.TutorialProgress;
            _lastLoginDateUtc = DateTime.TryParse(
                data.LastLoginDateUtc, null,
                System.Globalization.DateTimeStyles.RoundtripKind, out var parsed)
                ? parsed
                : DateTime.MinValue;
            _consecutiveLoginDays = data.ConsecutiveLoginDays;

            Turns = new TurnManager();
            Turns.LoadState(data.CurrentYear, data.CurrentWeek);
            BindTurnEvents();

            return true;
        }

        public IEnumerable<PlayerData> HallOfFameOB => PlayerTeam?.HallOfFame ?? Enumerable.Empty<PlayerData>();

        public bool IsFriendMatchUnlocked => HallOfFameOB.Count() >= 10;
    }
}
