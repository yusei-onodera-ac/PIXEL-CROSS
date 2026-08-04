using System;

namespace PixelCross.Data
{
    public enum ItemType
    {
        TrainingGear,
        RecoveryDrink,
        SkillBook
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public struct Item
    {
        public string Name;
        public ItemType Type;
        public ItemRarity Rarity;
        public float StatBonus;
        public int StaminaRecovery;
    }
}
