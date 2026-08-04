using System;

namespace PixelCross.Gacha
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
    public struct GachaItem
    {
        public string Name;
        public ItemType Type;
        public ItemRarity Rarity;
        public float StatBonus;
        public int StaminaRecovery;
    }
}
