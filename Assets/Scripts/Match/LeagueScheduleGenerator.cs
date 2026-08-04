using System;
using System.Collections.Generic;
using System.Linq;
using PixelCross.Core;
using PixelCross.Data;

namespace PixelCross.Match
{
    public static class LeagueScheduleGenerator
    {
        public const int DefaultOpponentCount = 6;

        // Lottery draw: pick a handful of opponents from all 20 schools and
        // assign each to a random week within the LeagueMatches phase.
        public static List<ScheduledMatch> GenerateLeagueSchedule(
            List<RivalSchoolData> allSchools, Random rng, int opponentCount = DefaultOpponentCount)
        {
            var availableWeeks = TurnManager.GetWeeksInYearForPhase(SeasonPhase.LeagueMatches);

            var selectedOpponents = allSchools
                .OrderBy(_ => rng.Next())
                .Take(Math.Min(opponentCount, allSchools.Count))
                .ToList();

            var selectedWeeks = availableWeeks
                .OrderBy(_ => rng.Next())
                .Take(selectedOpponents.Count)
                .OrderBy(week => week)
                .ToList();

            var schedule = new List<ScheduledMatch>();
            for (var i = 0; i < selectedWeeks.Count; i++)
            {
                schedule.Add(new ScheduledMatch(selectedWeeks[i], selectedOpponents[i]));
            }
            return schedule;
        }
    }
}
