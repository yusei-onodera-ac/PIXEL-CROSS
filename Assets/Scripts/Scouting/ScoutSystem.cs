using System;
using PixelCross.Data;

namespace PixelCross.Scouting
{
    public static class ScoutSystem
    {
        public const int DefaultTicketsPerYear = 2;

        public struct ScoutResult
        {
            public bool Success;
            public PlayerData Recruit;
        }

        public static ScoutResult TryScout(TeamData team, string prospectName, Random rng)
        {
            if (team.ScoutTickets <= 0)
            {
                return new ScoutResult { Success = false };
            }

            team.ScoutTickets--;

            var successChance = Math.Clamp(0.4f + team.Reputation / 200f, 0.1f, 0.95f);
            var success = rng.NextDouble() <= successChance;

            if (!success)
            {
                return new ScoutResult { Success = false };
            }

            var statFloor = 1f + team.Reputation / 100f;
            var recruit = new PlayerData(
                Guid.NewGuid().ToString("N"),
                prospectName,
                grade: 1,
                joinYear: 0,
                aptitude: PositionAptitude.Attack | PositionAptitude.Midfielder);

            recruit.Stats.Body = RandomizeStat(statFloor, rng);
            recruit.Stats.Speed = RandomizeStat(statFloor, rng);
            recruit.Stats.StaminaRank = RandomizeStat(statFloor, rng);
            recruit.Stats.Technique = RandomizeStat(statFloor, rng);
            recruit.Stats.Keeper = RandomizeStat(statFloor, rng);

            return new ScoutResult { Success = true, Recruit = recruit };
        }

        private static float RandomizeStat(float floor, Random rng)
        {
            var value = floor + (float)rng.NextDouble() * 2f;
            return Math.Clamp(value, PlayerStats.MinRank, PlayerStats.MaxRank);
        }
    }
}
