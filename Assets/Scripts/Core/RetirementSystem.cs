using System;
using System.Collections.Generic;
using System.Linq;
using PixelCross.Data;

namespace PixelCross.Core
{
    public static class RetirementSystem
    {
        public const float MaxProScoutChance = 0.9f;
        public const float ProScoutChanceScale = 0.6f;

        // Every graduating 4th-year is added to the OB roster (used for the
        // Hall of Fame encyclopedia and async friend-match defense teams);
        // pro-scouting is a separate, rarer honor rolled per player.
        public static List<PlayerData> ProcessGraduation(TeamData team, Random rng)
        {
            var graduates = team.Roster.Where(p => p.Grade >= 4 && !p.IsRetired).ToList();

            foreach (var player in graduates)
            {
                player.IsRetired = true;
                player.IsOB = true;
                team.HallOfFame.Add(player);

                var proScoutChance = Math.Clamp(player.OverallRank / PlayerStats.MaxRank * ProScoutChanceScale, 0f, MaxProScoutChance);
                if (rng.NextDouble() <= proScoutChance)
                {
                    player.IsProScouted = true;
                }
            }

            return graduates;
        }

        public static void AdvanceGradesForNewYear(TeamData team)
        {
            foreach (var player in team.ActiveRoster)
            {
                player.Grade++;
            }
        }
    }
}
