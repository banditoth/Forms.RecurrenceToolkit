using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using Xamarin.Essentials;

namespace banditoth.Forms.RecurrenceToolkit.Multilanguage
{
    public static class TranslationProvider
    {
        private const string PreferencesLanguageKey = "RecurrenceToolkit.Multilanguage.Language";

        public const string TranslationExceptionReturnPrefix = "TranslationError_";
        public const string TranslationNotFoundReturnPrefix = "TranslationMissing_";

        private static ResourceManager[] _translationResources = null;

        private static CultureInfo _defaultCulture = null;
        private static CultureInfo _currentCulture = null;

        public static void Initalize(CultureInfo defaultCulture, params ResourceManager[] resourceManagers)
        {
            if (defaultCulture == null)
                throw new ArgumentNullException(nameof(defaultCulture));

            if (resourceManagers == null)
                throw new ArgumentNullException(nameof(resourceManagers));

            if (resourceManagers.Length == 0)
                Debug.WriteLine("[TranslationProvider] warning: No resourcemaangers are provided");

            _translationResources = resourceManagers;
            _defaultCulture = defaultCulture;

            string storedLanguageCode = Preferences.Get(PreferencesLanguageKey, null);
            if (string.IsNullOrWhiteSpace(storedLanguageCode))
            {
                ChangeLanguage(_defaultCulture);
            }
            else if (storedLanguageCode != Thread.CurrentThread.CurrentUICulture.Name)
            {
                ChangeLanguage(new CultureInfo(storedLanguageCode));
            }
        }

        public static void ChangeLanguage(CultureInfo cultureChangeTo)
        {
            if (cultureChangeTo == null)
                throw new ArgumentNullException(nameof(cultureChangeTo));

            Thread.CurrentThread.CurrentUICulture = cultureChangeTo;
            Preferences.Set(PreferencesLanguageKey, Thread.CurrentThread.CurrentUICulture.Name);
            _currentCulture = cultureChangeTo;
            TranslationBinder.Instance.Invalidate();
        }

        public static CultureInfo GetCurrentLanguage()
        {
            return _currentCulture;
        }

        public static string GetTranslation(string translationKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(translationKey))
                    return null;

                string translationValue = _translationResources?.FirstOrDefault(resourceManager => string.IsNullOrWhiteSpace(resourceManager.GetString(translationKey, GetCurrentLanguage())) == false).GetString(translationKey, GetCurrentLanguage());

                if (string.IsNullOrWhiteSpace(translationValue))
                    return TranslationNotFoundReturnPrefix + translationKey;

                return translationValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[TranslationProvider] encountered an error: " + ex.ToString());
                return TranslationExceptionReturnPrefix + translationKey;
            }
        }
    }

}
