using System;
using System.Collections.Generic;
using System.Linq;
using PixelCross.Data;

namespace PixelCross.Match
{
    public static class IntercollegiateSystem
    {
        public const int FieldSize = 8;

        public class TournamentResult
        {
            public bool PlayerIsChampion;
            public string ChampionName;
            public int RoundsWon;
        }

        private class Participant
        {
            public string Name;
            public float Strength;
            public bool IsPlayer;
        }

        // The 5 powerhouse schools always qualify for nationals; remaining
        // slots (and the player's own slot) fill out an 8-team single-elim bracket.
        public static TournamentResult RunTournament(TeamData team, List<RivalSchoolData> allSchools, Random rng)
        {
            var powerhouses = allSchools.Where(s => s.IsPowerhouse).OrderBy(_ => rng.Next()).ToList();
            var others = allSchools.Where(s => !s.IsPowerhouse).OrderBy(_ => rng.Next()).ToList();

            var qualifierSlots = FieldSize - 1;
            var qualifiers = powerhouses.Take(qualifierSlots).ToList();
            if (qualifiers.Count < qualifierSlots)
            {
                qualifiers.AddRange(others.Take(qualifierSlots - qualifiers.Count));
            }

            var participants = qualifiers
                .Select(s => new Participant { Name = s.SchoolName, Strength = s.Strength, IsPlayer = false })
                .ToList();
            participants.Add(new Participant
            {
                Name = team.UniversityName,
                Strength = MatchSimulator.GetEffectiveStrength(team),
                IsPlayer = true
            });

            participants = participants.OrderBy(_ => rng.Next()).ToList();

            var roundsWon = 0;
            while (participants.Count > 1)
            {
                var nextRound = new List<Participant>();
                for (var i = 0; i < participants.Count; i += 2)
                {
                    var a = participants[i];
                    var b = participants[i + 1];
                    var result = MatchSimulator.SimulateByStrength(a.Strength, b.Strength, rng);
                    var winner = result.AwayScore > result.HomeScore ? b : a;

                    if (winner.IsPlayer)
                    {
                        roundsWon++;
                    }

                    nextRound.Add(winner);
                }
                participants = nextRound;
            }

            var champion = participants[0];
            return new TournamentResult
            {
                PlayerIsChampion = champion.IsPlayer,
                ChampionName = champion.Name,
                RoundsWon = roundsWon
            };
        }
    }
}
