using System;

namespace PixelCross.Data
{
    [Flags]
    public enum PositionAptitude
    {
        None = 0,
        Attack = 1 << 0,
        Midfielder = 1 << 1,
        Defense = 1 << 2,
        Goalkeeper = 1 << 3
    }
}
