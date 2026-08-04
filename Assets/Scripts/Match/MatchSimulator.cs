using System;
using PixelCross.Data;

namespace PixelCross.Match
{
    // Placeholder outcome model for scheduling/testing; the real top-down action
    // simulation (per the design doc's in-match visuals) replaces this later.
    public static class MatchSimulator
    {
        public static MatchResult SimulateAgainstRival(TeamData home, RivalSchoolData away, Random rng) =>
            SimulateByStrength(GetEffectiveStrength(home), away.Strength, rng);

        public static MatchResult SimulateByStrength(float homeStrength, float awayStrength, Random rng)
        {
            var homeScore = ScoreFromStrength(homeStrength, rng);
            var awayScore = ScoreFromStrength(awayStrength, rng);
            return new MatchResult { HomeScore = homeScore, AwayScore = awayScore };
        }

        public static float GetEffectiveStrength(TeamData team) => team.TeamStrength * TacticsMultiplier(team.Tactics);

        private static float TacticsMultiplier(TacticsSettings tactics)
        {
            var offenseBonus = tactics.Offense switch
            {
                OffenseStyle.SuperAggressive => 1.15f,
                OffenseStyle.LongShotFocus => 1.05f,
                _ => 1.0f
            };

            var defenseBonus = tactics.Defense switch
            {
                DefenseStyle.HighPress => 1.05f,
                DefenseStyle.ZoneDefense => 1.02f,
                _ => 1.0f
            };

            return offenseBonus * defenseBonus;
        }

        private static int ScoreFromStrength(float strength, Random rng)
        {
            var expected = Math.Max(1f, strength / 10f);
            var variance = (float)(rng.NextDouble() * 2 - 1) * expected * 0.3f;
            return Math.Max(0, (int)Math.Round(expected + variance));
        }
    }
}
