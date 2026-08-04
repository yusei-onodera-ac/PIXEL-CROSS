using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelCross.Data
{
    [Serializable]
    public class TeamData
    {
        public string UniversityName;
        public string ManagerName;
        public int Reputation;
        public int NationalRanking;

        public List<PlayerData> Roster = new List<PlayerData>();
        public List<PlayerData> HallOfFame = new List<PlayerData>();
        public TacticsSettings Tactics = new TacticsSettings();

        public int ScoutTickets;
        public int GachaTickets;
        public int GachaCurrency;

        public TeamData()
        {
        }

        public TeamData(string universityName, string managerName)
        {
            UniversityName = universityName;
            ManagerName = managerName;
        }

        public IEnumerable<PlayerData> ActiveRoster => Roster.Where(p => !p.IsRetired);

        public float TeamStrength =>
            ActiveRoster.Any() ? ActiveRoster.Average(p => p.OverallRank) * 20f : 0f;
    }
}
