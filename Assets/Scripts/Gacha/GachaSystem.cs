using System;
using System.Collections.Generic;

namespace PixelCross.Gacha
{
    public static class GachaSystem
    {
        public const int SinglePullCost = 100;
        public const int TenPullCost = 900;

        private static readonly (ItemRarity Rarity, float Weight)[] RarityTable =
        {
            (ItemRarity.Common, 0.60f),
            (ItemRarity.Rare, 0.28f),
            (ItemRarity.Epic, 0.10f),
            (ItemRarity.Legendary, 0.02f)
        };

        public static GachaItem PullSingle(Random rng)
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

        public static List<GachaItem> PullTen(Random rng)
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
