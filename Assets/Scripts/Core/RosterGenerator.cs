using System;
using System.Collections.Generic;
using PixelCross.Data;

namespace PixelCross.Core
{
    public static class RosterGenerator
    {
        public const int InitialPlayersPerGrade = 3;
        public const int YearlyFreshmenCount = 5;

        public static List<PlayerData> GenerateInitialRoster(int currentYear, Random rng)
        {
            var roster = new List<PlayerData>();
            for (var grade = 1; grade <= 4; grade++)
            {
                for (var i = 0; i < InitialPlayersPerGrade; i++)
                {
                    roster.Add(CreatePlayer(grade, currentYear - (grade - 1), rng));
                }
            }
            return roster;
        }

        public static List<PlayerData> GenerateFreshmenIntake(int currentYear, Random rng, int count = YearlyFreshmenCount)
        {
            var freshmen = new List<PlayerData>();
            for (var i = 0; i < count; i++)
            {
                freshmen.Add(CreatePlayer(grade: 1, joinYear: currentYear, rng));
            }
            return freshmen;
        }

        private static PlayerData CreatePlayer(int grade, int joinYear, Random rng)
        {
            var player = new PlayerData(
                Guid.NewGuid().ToString("N"),
                PlayerNameGenerator.GenerateName(rng),
                grade,
                joinYear,
                RollAptitude(rng));

            var startingFloor = 1f + (grade - 1) * 0.3f;
            player.Stats.Body = RandomizeStat(startingFloor, rng);
            player.Stats.Speed = RandomizeStat(startingFloor, rng);
            player.Stats.StaminaRank = RandomizeStat(startingFloor, rng);
            player.Stats.Technique = RandomizeStat(startingFloor, rng);
            player.Stats.Keeper = player.Aptitude == PositionAptitude.Goalkeeper
                ? RandomizeStat(startingFloor + 0.5f, rng)
                : RandomizeStat(1f, rng);

            return player;
        }

        private static PositionAptitude RollAptitude(Random rng)
        {
            var roll = rng.Next(0, 100);
            return roll switch
            {
                < 10 => PositionAptitude.Goalkeeper,
                < 40 => PositionAptitude.Attack,
                < 70 => PositionAptitude.Midfielder,
                _ => PositionAptitude.Defense
            };
        }

        private static float RandomizeStat(float floor, Random rng)
        {
            var value = floor + (float)rng.NextDouble() * 1.5f;
            return Math.Clamp(value, PlayerStats.MinRank, PlayerStats.MaxRank);
        }
    }
}
