using PixelCross.Data;

namespace PixelCross.Training
{
    public static class TrainingSystem
    {
        public const int StaminaCostPerSession = 15;
        public const float GrowthPerSession = 0.1f;

        public struct TrainingResult
        {
            public bool Success;
            public string GrownStatName;
        }

        public static TrainingResult ApplyTraining(PlayerData player, TrainingMenu menu)
        {
            if (!player.Stats.SpendStamina(StaminaCostPerSession))
            {
                return new TrainingResult { Success = false };
            }

            switch (menu)
            {
                case TrainingMenu.StrengthTraining:
                    player.Stats.Body = Clamp(player.Stats.Body + GrowthPerSession);
                    return Result("Body");

                case TrainingMenu.RunningAgility:
                    player.Stats.Speed = Clamp(player.Stats.Speed + GrowthPerSession);
                    player.Stats.StaminaRank = Clamp(player.Stats.StaminaRank + GrowthPerSession);
                    return Result("Speed/Stamina");

                case TrainingMenu.TacticsPassDrill:
                    player.Stats.Technique = Clamp(player.Stats.Technique + GrowthPerSession);
                    return Result("Technique");

                case TrainingMenu.GoalkeeperTraining:
                    player.Stats.Keeper = Clamp(player.Stats.Keeper + GrowthPerSession);
                    return Result("Keeper");

                default:
                    return new TrainingResult { Success = false };
            }
        }

        private static TrainingResult Result(string statName) => new TrainingResult { Success = true, GrownStatName = statName };

        private static float Clamp(float value) =>
            value < PlayerStats.MinRank ? PlayerStats.MinRank : value > PlayerStats.MaxRank ? PlayerStats.MaxRank : value;
    }
}
