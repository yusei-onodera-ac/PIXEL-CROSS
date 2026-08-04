using System;
using System.Collections.Generic;
using PixelCross.Data;

namespace PixelCross.Gacha
{
    public static class GachaSystem
    {
        public const int SinglePullTicketCost = 1;
        public const int TenPullTicketCost = 10;
        public const int SinglePullPremiumCost = 50;
        public const int TenPullPremiumCost = 450;

        private static readonly (ItemRarity Rarity, float Weight)[] RarityTable =
        {
            (ItemRarity.Common, 0.60f),
            (ItemRarity.Rare, 0.28f),
            (ItemRarity.Epic, 0.10f),
            (ItemRarity.Legendary, 0.02f)
        };

        public static bool TryPullSingleWithTicket(TeamData team, Random rng, out GachaItem item)
        {
            if (team.GachaTickets < SinglePullTicketCost)
            {
                item = default;
                return false;
            }

            team.GachaTickets -= SinglePullTicketCost;
            item = PullSingle(rng);
            return true;
        }

        public static bool TryPullTenWithTicket(TeamData team, Random rng, out List<GachaItem> items)
        {
            if (team.GachaTickets < TenPullTicketCost)
            {
                items = null;
                return false;
            }

            team.GachaTickets -= TenPullTicketCost;
            items = PullTen(rng);
            return true;
        }

        public static bool TryPullSingleWithPremiumCurrency(TeamData team, Random rng, out GachaItem item)
        {
            if (team.PremiumCurrency < SinglePullPremiumCost)
            {
                item = default;
                return false;
            }

            team.PremiumCurrency -= SinglePullPremiumCost;
            item = PullSingle(rng);
            return true;
        }

        public static bool TryPullTenWithPremiumCurrency(TeamData team, Random rng, out List<GachaItem> items)
        {
            if (team.PremiumCurrency < TenPullPremiumCost)
            {
                items = null;
                return false;
            }

            team.PremiumCurrency -= TenPullPremiumCost;
            items = PullTen(rng);
            return true;
        }

        private static GachaItem PullSingle(Random rng)
        {
            var roll = rng.NextDouble();
            var cumulative = 0f;
            var rarity = ItemRarity.Common;

            foreach (var (r, weight) in RarityTable)
            {
                cumulative += weight;
                if (roll <= cumulative)
                {
                    rarity = r;
                    break;
                }
            }

            return BuildItem(rarity, rng);
        }

        private static List<GachaItem> PullTen(Random rng)
        {
            var results = new List<GachaItem>(10);
            for (var i = 0; i < 10; i++)
            {
                results.Add(PullSingle(rng));
            }
            return results;
        }

        private static GachaItem BuildItem(ItemRarity rarity, Random rng)
        {
            var itemType = (ItemType)rng.Next(0, 3);
            var multiplier = rarity switch
            {
                ItemRarity.Common => 1f,
                ItemRarity.Rare => 2f,
                ItemRarity.Epic => 3.5f,
                ItemRarity.Legendary => 6f,
                _ => 1f
            };

            return new GachaItem
            {
                Name = $"{rarity} {itemType}",
                Type = itemType,
                Rarity = rarity,
                StatBonus = itemType == ItemType.SkillBook ? 0.1f * multiplier : 0f,
                StaminaRecovery = itemType == ItemType.RecoveryDrink ? (int)(10 * multiplier) : 0
            };
        }
    }
}
