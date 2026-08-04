using System.Collections.Generic;
using System.Linq;
using PixelCross.Data;
using PixelCross.SaveLoad;
using PixelCross.Tutorial;
using UnityEngine;

namespace PixelCross.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public TurnManager Turns { get; private set; } = new TurnManager();
        public TeamData PlayerTeam { get; private set; }
        public List<RivalSchoolData> RivalSchools { get; private set; } = new List<RivalSchoolData>();
        public TutorialStep TutorialProgress { get; set; } = TutorialStep.NotStarted;

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

        public void StartNewGame(string universityName, string managerName)
        {
            PlayerTeam = new TeamData(universityName, managerName)
            {
                ScoutTickets = 2,
                Reputation = 10,
                NationalRanking = 20
            };
            RivalSchools = RivalSchoolDatabase.CreateDefaultSchools();
            Turns = new TurnManager();
            TutorialProgress = TutorialStep.NotStarted;
        }

        public void AdvanceWeek()
        {
            Turns.AdvanceWeek();
        }

        public void SaveGame(string slot = "default")
        {
            var data = new GameSaveData
            {
                CurrentYear = Turns.CurrentYear,
                CurrentWeek = Turns.CurrentWeek,
                PlayerTeam = PlayerTeam,
                RivalSchools = RivalSchools,
                TutorialProgress = TutorialProgress
            };
            SaveSystem.Save(data, slot);
        }

        public bool LoadGame(string slot = "default")
        {
            if (!SaveSystem.TryLoad(slot, out var data)) return false;

            PlayerTeam = data.PlayerTeam;
            RivalSchools = data.RivalSchools;
            TutorialProgress = data.TutorialProgress;

            Turns = new TurnManager();
            while (Turns.CurrentYear != data.CurrentYear || Turns.CurrentWeek != data.CurrentWeek)
            {
                Turns.AdvanceWeek();
            }

            return true;
        }

        public IEnumerable<PlayerData> HallOfFameOB => PlayerTeam?.HallOfFame ?? Enumerable.Empty<PlayerData>();

        public bool IsFriendMatchUnlocked => HallOfFameOB.Count() >= 10;
    }
}
