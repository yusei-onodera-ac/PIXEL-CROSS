using System.Collections.Generic;
using PixelCross.Data;

namespace PixelCross.Inventory
{
    // Fixed, non-random catalog (unlike the gacha) so BasicCurrency always
    // has a predictable, guaranteed way to get useful items. Prices are
    // placeholders pending economy balancing.
    public static class ItemShopSystem
    {
        public struct ShopEntry
        {
            public string Name;
            public ItemType Type;
            public ItemRarity Rarity;
            public float StatBonus;
            public int StaminaRecovery;
            public int Price;
        }

        private static readonly ShopEntry[] Catalog =
        {
            new ShopEntry
            {
                Name = "回復ドリンク", Type = ItemType.RecoveryDrink, Rarity = ItemRarity.Common,
                StaminaRecovery = 15, Price = 50
            },
            new ShopEntry
            {
                Name = "スキルブック", Type = ItemType.SkillBook, Rarity = ItemRarity.Common,
                StatBonus = 0.15f, Price = 150
            },
            new ShopEntry
            {
                Name = "トレーニング用具", Type = ItemType.TrainingGear, Rarity = ItemRarity.Common,
                StatBonus = 0.15f, Price = 150
            }
        };

        public static IReadOnlyList<ShopEntry> GetCatalog() => Catalog;

        public static bool TryPurchase(TeamData team, int catalogIndex, out Item purchasedItem)
        {
            if (catalogIndex < 0 || catalogIndex >= Catalog.Length)
            {
                purchasedItem = default;
                return false;
            }

            var entry = Catalog[catalogIndex];
            if (team.BasicCurrency < entry.Price)
            {
                purchasedItem = default;
                return false;
            }

            team.BasicCurrency -= entry.Price;
            purchasedItem = new Item
            {
                Name = entry.Name,
                Type = entry.Type,
                Rarity = entry.Rarity,
                StatBonus = entry.StatBonus,
                StaminaRecovery = entry.StaminaRecovery
            };

            InventorySystem.AddItem(team, purchasedItem);
            return true;
        }
    }
}
