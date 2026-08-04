using System;
using System.Collections.Generic;
using PixelCross.Data;
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

        public TutorialStep TutorialProgress = TutorialStep.NotStarted;

        public string LanguageCode = "ja";
    }
}
