using System;

namespace PixelCross.Data
{
    public enum OffenseStyle
    {
        SuperAggressive,
        ShortPassFocus,
        LongShotFocus
    }

    public enum DefenseStyle
    {
        ManToMan,
        ZoneDefense,
        HighPress
    }

    [Serializable]
    public class TacticsSettings
    {
        public OffenseStyle Offense = OffenseStyle.ShortPassFocus;
        public DefenseStyle Defense = DefenseStyle.ManToMan;
        public bool AutoPlay = true;
    }
}
