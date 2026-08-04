using System;
using UnityEngine.Localization.Settings;

namespace PixelCross.Localization
{
    // Thin wrapper over com.unity.localization so gameplay code doesn't
    // depend on the package API directly.
    public static class LocalizationManager
    {
        public static void SetLanguage(SupportedLanguage language)
        {
            var localeCode = SupportedLanguageInfo.ToLocaleCode(language);
            var locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
            }
        }

        public static string GetString(string table, string key) =>
            LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
    }
}
