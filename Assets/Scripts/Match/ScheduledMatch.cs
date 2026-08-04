using System;
using PixelCross.Data;

namespace PixelCross.Match
{
    [Serializable]
    public class ScheduledMatch
    {
        public int Week;
        public RivalSchoolData Opponent;
        public bool IsIntercollegiate;

        public ScheduledMatch(int week, RivalSchoolData opponent, bool isIntercollegiate = false)
        {
            Week = week;
            Opponent = opponent;
            IsIntercollegiate = isIntercollegiate;
        }
    }
}
