namespace PixelCross.Localization
{
    // Matches the 16 nations at the 2026 World Lacrosse Women's Championship
    // (Tokyo, Jul 24 - Aug 2, 2026), consolidating shared languages:
    // Japan->ja, England/Scotland/Wales/Ireland/Australia/USA->en,
    // Israel->he, Germany->de, Czech Republic->cs, Chinese Taipei->zh-Hant,
    // Philippines->fil, Canada->fr (+en), Argentina/Puerto Rico->es.
    // Haudenosaunee (Iroquois Nationals) has no single national language
    // code and is not covered here - falls back to English.
    public enum SupportedLanguage
    {
        Japanese,
        English,
        French,
        Spanish,
        German,
        Czech,
        Hebrew,
        ChineseTraditional,
        Filipino
    }

    public static class SupportedLanguageInfo
    {
        public static string ToLocaleCode(SupportedLanguage language) => language switch
        {
            SupportedLanguage.Japanese => "ja",
            SupportedLanguage.English => "en",
            SupportedLanguage.French => "fr",
            SupportedLanguage.Spanish => "es",
            SupportedLanguage.German => "de",
            SupportedLanguage.Czech => "cs",
            SupportedLanguage.Hebrew => "he",
            SupportedLanguage.ChineseTraditional => "zh-Hant",
            SupportedLanguage.Filipino => "fil",
            _ => "en"
        };

        // Hebrew renders right-to-left; TextMeshPro needs an RTL shaping
        // plugin (not included in Packages/manifest.json) before Hebrew UI
        // will actually lay out correctly.
        public static bool IsRightToLeft(SupportedLanguage language) =>
            language == SupportedLanguage.Hebrew;
    }
}
