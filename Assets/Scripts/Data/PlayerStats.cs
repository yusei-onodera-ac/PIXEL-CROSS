using System;

namespace PixelCross.Data
{
    [Serializable]
    public class PlayerStats
    {
        public const float MinRank = 1f;
        public const float MaxRank = 5f;
        public const int StaminaPointsPerRank = 20;

        public float Body = 1f;
        public float Speed = 1f;
        public float StaminaRank = 1f;
        public float Technique = 1f;
        public float Keeper = 1f;

        public int CurrentStaminaPoints;
        public int MaxStaminaPoints => Mathf(StaminaRank) * StaminaPointsPerRank;

        public PlayerStats()
        {
            CurrentStaminaPoints = MaxStaminaPoints;
        }

        public int BodyRank => Mathf(Body);
        public int SpeedRank => Mathf(Speed);
        public int StaminaDisplayRank => Mathf(StaminaRank);
        public int TechniqueRank => Mathf(Technique);
        public int KeeperRank => Mathf(Keeper);

        private static int Mathf(float value) => (int)Math.Round(Math.Clamp(value, MinRank, MaxRank));

        public void RecoverStamina(int amount)
        {
            CurrentStaminaPoints = Math.Min(MaxStaminaPoints, CurrentStaminaPoints + amount);
        }

        public bool SpendStamina(int amount)
        {
            if (CurrentStaminaPoints < amount) return false;
            CurrentStaminaPoints -= amount;
            return true;
        }
    }
}
