namespace PixelCross.Localization
{
    // Matches the 16 nations at the 2026 World Lacrosse Women's Championship
    // (Tokyo, Jul 24 - Aug 2, 2026), consolidating shared languages:
    // Japan->ja, England/Scotland/Wales/Ireland/Australia/USA->en,
    // Germany->de, Czech Republic->cs, Chinese Taipei->zh-Hant,
    // Philippines->fil, Canada->fr (+en), Argentina/Puerto Rico->es.
    // Israel (Hebrew) is intentionally excluded to avoid RTL text-shaping
    // work. Haudenosaunee (Iroquois Nationals) has no single national
    // language code and is not covered either - both fall back to English.
    public enum SupportedLanguage
    {
        Japanese,
        English,
        French,
        Spanish,
        German,
        Czech,
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
            SupportedLanguage.ChineseTraditional => "zh-Hant",
            SupportedLanguage.Filipino => "fil",
            _ => "en"
        };
    }
}
