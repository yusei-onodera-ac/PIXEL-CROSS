using System;
using System.Collections.Generic;
using PixelCross.Data;
using PixelCross.Match;
using PixelCross.Tutorial;

namespace PixelCross.SaveLoad
{
    [Serializable]
    public class GameSaveData
    {
        public int CurrentYear;
        public int CurrentWeek;

        public TeamData PlayerTeam;
        public List<RivalSchoolData> RivalSchools = new List<RivalSchoolData>();
        public List<ScheduledMatch> CurrentYearSchedule = new List<ScheduledMatch>();

        public TutorialStep TutorialProgress = TutorialStep.NotStarted;

        // ISO 8601 string: JsonUtility cannot serialize System.DateTime directly.
        public string LastLoginDateUtc = DateTime.MinValue.ToString("o");
        public int ConsecutiveLoginDays;

        public string LanguageCode = "ja";
    }
}
