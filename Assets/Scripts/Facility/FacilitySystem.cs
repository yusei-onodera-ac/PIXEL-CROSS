using PixelCross.Data;

namespace PixelCross.Facility
{
    // Structural skeleton only: upgrading spends BasicCurrency and raises a
    // level, but no gameplay system (training/scouting/stamina/match) reads
    // these levels yet. Wire in effects once facility design is finalized.
    public static class FacilitySystem
    {
        public const int MaxLevel = 5;
        public const int BaseUpgradeCost = 500;

        public static int GetLevel(FacilityLevels facilities, FacilityType type) => type switch
        {
            FacilityType.TrainingGround => facilities.TrainingGround,
            FacilityType.MedicalRoom => facilities.MedicalRoom,
            FacilityType.Dormitory => facilities.Dormitory,
            FacilityType.VideoAnalysisRoom => facilities.VideoAnalysisRoom,
            _ => 0
        };

        public static int GetUpgradeCost(int currentLevel) => currentLevel * BaseUpgradeCost;

        public static bool TryUpgrade(TeamData team, FacilityType type, out int newLevel)
        {
            var currentLevel = GetLevel(team.Facilities, type);

            if (currentLevel >= MaxLevel)
            {
                newLevel = currentLevel;
                return false;
            }

            var cost = GetUpgradeCost(currentLevel);
            if (team.BasicCurrency < cost)
            {
                newLevel = currentLevel;
                return false;
            }

            team.BasicCurrency -= cost;
            newLevel = currentLevel + 1;
            SetLevel(team.Facilities, type, newLevel);
            return true;
        }

        private static void SetLevel(FacilityLevels facilities, FacilityType type, int level)
        {
            switch (type)
            {
                case FacilityType.TrainingGround:
                    facilities.TrainingGround = level;
                    break;
                case FacilityType.MedicalRoom:
                    facilities.MedicalRoom = level;
                    break;
                case FacilityType.Dormitory:
                    facilities.Dormitory = level;
                    break;
                case FacilityType.VideoAnalysisRoom:
                    facilities.VideoAnalysisRoom = level;
                    break;
            }
        }
    }
}
