using System;

namespace PixelCross.Match
{
    [Serializable]
    public struct MatchResult
    {
        public int HomeScore;
        public int AwayScore;
        public bool HomeWon => HomeScore > AwayScore;
        public bool IsDraw => HomeScore == AwayScore;
    }
}
