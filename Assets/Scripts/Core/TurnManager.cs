using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelCross.Core
{
    public class TurnManager
    {
        public const int WeeksPerMonth = 4;
        public const int MonthsPerYear = 12;
        public const int WeeksPerYear = WeeksPerMonth * MonthsPerYear;

        public int CurrentYear { get; private set; } = 1;
        public int CurrentWeek { get; private set; } = 1;

        public event Action<int, int> OnWeekAdvanced;
        public event Action<SeasonPhase> OnPhaseChanged;
        public event Action<int> OnNewYearStarted;
        public event Action<int> OnIntercollegiateFinished;

        private SeasonPhase _previousPhase;

        public TurnManager()
        {
            _previousPhase = GetPhaseForMonth(GetMonth(CurrentWeek));
        }

        public int GetMonth(int week) => ((week - 1) / WeeksPerMonth) % MonthsPerYear + 1;

        public SeasonPhase CurrentPhase => GetPhaseForMonth(GetMonth(CurrentWeek));

        public bool IsScoutingWindowOpen => CurrentPhase == SeasonPhase.SummerCampAndScouting;

        public static SeasonPhase GetPhaseForMonth(int month)
        {
            // Month 1 = April, following the university academic year used in the design doc.
            return month switch
            {
                1 => SeasonPhase.NewRecruitsIntake,
                2 or 3 => SeasonPhase.FreshmanCup,
                4 or 5 => SeasonPhase.SummerCampAndScouting,
                6 or 7 => SeasonPhase.LeagueMatches,
                8 => SeasonPhase.Intercollegiate,
                _ => SeasonPhase.OffSeason
            };
        }

        public static (int StartWeek, int EndWeek) GetWeekRangeForMonth(int month)
        {
            var start = (month - 1) * WeeksPerMonth + 1;
            return (start, start + WeeksPerMonth - 1);
        }

        public static IReadOnlyList<int> GetWeeksInYearForPhase(SeasonPhase phase)
        {
            var weeks = new List<int>();
            for (var month = 1; month <= MonthsPerYear; month++)
            {
                if (GetPhaseForMonth(month) != phase) continue;
                var (start, end) = GetWeekRangeForMonth(month);
                weeks.AddRange(Enumerable.Range(start, end - start + 1));
            }
            return weeks;
        }

        // Restores year/week without firing week/phase events, for use when
        // reconstructing state from a save file (event-driven side effects
        // like freshmen intake must not replay during a load).
        public void LoadState(int year, int week)
        {
            CurrentYear = year;
            CurrentWeek = week;
            _previousPhase = CurrentPhase;
        }

        public void AdvanceWeek()
        {
            var wasIntercollegiate = CurrentPhase == SeasonPhase.Intercollegiate;

            CurrentWeek++;
            if (CurrentWeek > WeeksPerYear)
            {
                CurrentWeek = 1;
                CurrentYear++;
            }

            OnWeekAdvanced?.Invoke(CurrentWeek, CurrentYear);

            var newPhase = CurrentPhase;
            if (newPhase != _previousPhase)
            {
                if (wasIntercollegiate && newPhase == SeasonPhase.OffSeason)
                {
                    OnIntercollegiateFinished?.Invoke(CurrentYear);
                }

                if (newPhase == SeasonPhase.NewRecruitsIntake)
                {
                    OnNewYearStarted?.Invoke(CurrentYear);
                }

                _previousPhase = newPhase;
                OnPhaseChanged?.Invoke(newPhase);
            }
        }
    }
}
