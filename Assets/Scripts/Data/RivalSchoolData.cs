using System;

namespace PixelCross.Data
{
    [Serializable]
    public class RivalSchoolData
    {
        public string SchoolName;
        public int Strength;
        public int Reputation;
        public bool IsPowerhouse;
        public string TeamColorHex;

        public RivalSchoolData()
        {
        }

        public RivalSchoolData(string schoolName, int strength, int reputation, bool isPowerhouse, string teamColorHex)
        {
            SchoolName = schoolName;
            Strength = strength;
            Reputation = reputation;
            IsPowerhouse = isPowerhouse;
            TeamColorHex = teamColorHex;
        }
    }
}
