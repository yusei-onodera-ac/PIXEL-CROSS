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
        public FacilityLevels Facilities = new FacilityLevels();

        public int ScoutTickets;
        public int GachaTickets;

        // Two-tier currency: BasicCurrency is earned in-game (match wins,
        // events) and spent on the item shop / facility upgrades.
        // PremiumCurrency comes from IAP or the daily login bonus, and can
        // buy gacha pulls or be exchanged for BasicCurrency (never the reverse).
        public int BasicCurrency;
        public int PremiumCurrency;

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
