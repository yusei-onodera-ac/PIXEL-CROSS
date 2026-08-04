using System;

namespace PixelCross.Data
{
    // Default name pool assumes a Japanese university setting (Kanto/Kansai
    // student lacrosse leagues per the design doc). Swap/extend this if
    // localized rosters are needed for overseas builds.
    public static class PlayerNameGenerator
    {
        private static readonly string[] Surnames =
        {
            "佐藤", "鈴木", "高橋", "田中", "伊藤", "渡辺", "山本", "中村", "小林", "加藤",
            "吉田", "山田", "佐々木", "松本", "井上", "木村", "林", "斎藤", "清水", "山口"
        };

        private static readonly string[] GivenNames =
        {
            "翔太", "大輝", "拓海", "陸", "颯太", "蓮", "悠斗", "海斗", "健太", "亮太",
            "直樹", "翼", "駿", "光", "遥", "隼人", "優斗", "大和", "凌", "楓"
        };

        public static string GenerateName(Random rng)
        {
            var surname = Surnames[rng.Next(Surnames.Length)];
            var givenName = GivenNames[rng.Next(GivenNames.Length)];
            return $"{surname}{givenName}";
        }
    }
}
