using System;
using PixelCross.Data;

namespace PixelCross.Economy
{
    public static class LoginBonusSystem
    {
        public const int CycleLength = 10;

        // Day 1..10 reward in premium currency; repeats every 10 days.
        // Placeholder curve based on the requested "1,2,2,...,3" shape - tune later.
        private static readonly int[] DailyRewardTable = { 1, 2, 2, 2, 2, 2, 2, 2, 2, 3 };

        public struct LoginResult
        {
            public bool BonusGranted;
            public int ConsecutiveDay;
            public int PremiumCurrencyGranted;
        }

        public static LoginResult ProcessLogin(
            TeamData team, ref DateTime lastLoginDateUtc, ref int consecutiveDays, DateTime nowUtc)
        {
            var today = nowUtc.Date;
            var lastDate = lastLoginDateUtc.Date;
            var alreadyClaimedToday = consecutiveDays > 0 && today == lastDate;

            if (alreadyClaimedToday)
            {
                return new LoginResult { BonusGranted = false, ConsecutiveDay = consecutiveDays };
            }

            var isConsecutive = consecutiveDays > 0 && today == lastDate.AddDays(1);
            consecutiveDays = isConsecutive ? consecutiveDays + 1 : 1;
            lastLoginDateUtc = today;

            var reward = DailyRewardTable[(consecutiveDays - 1) % CycleLength];
            team.PremiumCurrency += reward;

            return new LoginResult
            {
                BonusGranted = true,
                ConsecutiveDay = consecutiveDays,
                PremiumCurrencyGranted = reward
            };
        }
    }
}
