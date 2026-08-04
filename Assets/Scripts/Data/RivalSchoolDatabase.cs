using System.Collections.Generic;

namespace PixelCross.Data
{
    public static class RivalSchoolDatabase
    {
        public static List<RivalSchoolData> CreateDefaultSchools()
        {
            var schools = new List<RivalSchoolData>
            {
                new RivalSchoolData("Crimson Eagles University", 95, 90, true, "#B22222"),
                new RivalSchoolData("Northern Wolves University", 92, 88, true, "#1E3A8A"),
                new RivalSchoolData("Golden Titans University", 90, 85, true, "#D4AF37"),
                new RivalSchoolData("Blue Phoenix University", 88, 87, true, "#2563EB"),
                new RivalSchoolData("Iron Bears University", 87, 82, true, "#4B5563"),

                new RivalSchoolData("Seaside University", 70, 60, false, "#0EA5E9"),
                new RivalSchoolData("Green Valley University", 68, 58, false, "#22C55E"),
                new RivalSchoolData("Riverside University", 65, 55, false, "#14B8A6"),
                new RivalSchoolData("Highland University", 63, 52, false, "#A855F7"),
                new RivalSchoolData("Sunrise University", 60, 50, false, "#F97316"),
                new RivalSchoolData("Silver Lake University", 58, 48, false, "#94A3B8"),
                new RivalSchoolData("Maple University", 55, 45, false, "#DC2626"),
                new RivalSchoolData("Coral Bay University", 53, 42, false, "#F43F5E"),
                new RivalSchoolData("Starlight University", 50, 40, false, "#6366F1"),
                new RivalSchoolData("Prairie University", 48, 38, false, "#84CC16"),
                new RivalSchoolData("Harbor University", 45, 35, false, "#0891B2"),
                new RivalSchoolData("Willow University", 42, 32, false, "#65A30D"),
                new RivalSchoolData("Frontier University", 40, 30, false, "#7C3AED"),
                new RivalSchoolData("Bayview University", 37, 28, false, "#059669"),
                new RivalSchoolData("Meadowbrook University", 35, 25, false, "#EA580C"),
            };

            return schools;
        }
    }
}
