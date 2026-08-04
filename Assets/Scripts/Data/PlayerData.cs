using System;

namespace PixelCross.Data
{
    [Serializable]
    public class PlayerData
    {
        public string Id;
        public string Name;
        public int Grade;
        public int JoinYear;
        public PositionAptitude Aptitude;
        public PlayerStats Stats = new PlayerStats();

        public bool IsRetired;
        public bool IsOB;
        public bool IsProScouted;
        public bool IsHallOfFame;

        public PlayerData()
        {
        }

        public PlayerData(string id, string name, int grade, int joinYear, PositionAptitude aptitude)
        {
            Id = id;
            Name = name;
            Grade = grade;
            JoinYear = joinYear;
            Aptitude = aptitude;
        }

        public float OverallRank =>
            (Stats.BodyRank + Stats.SpeedRank + Stats.StaminaDisplayRank + Stats.TechniqueRank + Stats.KeeperRank) / 5f;
    }
}
