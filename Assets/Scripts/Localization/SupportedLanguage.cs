namespace PixelCross.Localization
{
    // Prioritizes regions where lacrosse has an established player/fan base.
    public enum SupportedLanguage
    {
        Japanese,
        English,
        French
    }

    public static class SupportedLanguageInfo
    {
        public static string ToLocaleCode(SupportedLanguage language) => language switch
        {
            SupportedLanguage.Japanese => "ja",
            SupportedLanguage.English => "en",
            SupportedLanguage.French => "fr",
            _ => "en"
        };
    }
}
