using System.Collections.Generic;
using PixelCross.Data;

namespace PixelCross.Inventory
{
    public static class InventorySystem
    {
        public static void AddItem(TeamData team, Item item) => team.Inventory.Add(item);

        public static void AddItems(TeamData team, List<Item> items) => team.Inventory.AddRange(items);

        // Effect mapping is a placeholder: SkillBook -> Technique, TrainingGear
        // -> Body, RecoveryDrink -> stamina. Revisit once item design is final.
        public static bool UseItem(TeamData team, PlayerData player, int inventoryIndex)
        {
            if (inventoryIndex < 0 || inventoryIndex >= team.Inventory.Count) return false;

            var item = team.Inventory[inventoryIndex];
            switch (item.Type)
            {
                case ItemType.RecoveryDrink:
                    player.Stats.RecoverStamina(item.StaminaRecovery);
                    break;
                case ItemType.SkillBook:
                    player.Stats.Technique = ClampStat(player.Stats.Technique + item.StatBonus);
                    break;
                case ItemType.TrainingGear:
                    player.Stats.Body = ClampStat(player.Stats.Body + item.StatBonus);
                    break;
            }

            team.Inventory.RemoveAt(inventoryIndex);
            return true;
        }

        private static float ClampStat(float value) =>
            value < PlayerStats.MinRank ? PlayerStats.MinRank : value > PlayerStats.MaxRank ? PlayerStats.MaxRank : value;
    }
}
