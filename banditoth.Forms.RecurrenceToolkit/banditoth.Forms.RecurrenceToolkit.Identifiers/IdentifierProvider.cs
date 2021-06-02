using System;
using Xamarin.Essentials;

namespace banditoth.Forms.RecurrenceToolkit.Identifiers
{
    public static class IdentifierProvider
    {
        private const string UniqueIdentifierPreferenceKey = "IDENTIFIERPROVIDER_UNIQUEID";

        public static string UniqueId
        {
            get
            {
                string stored = Preferences.Get(UniqueIdentifierPreferenceKey, null);
                if ((VersionTracking.IsFirstLaunchEver && string.IsNullOrWhiteSpace(stored)) || string.IsNullOrWhiteSpace(stored))
                {
                    stored = Guid.NewGuid().ToString();
                    Preferences.Set(UniqueIdentifierPreferenceKey, stored);
                }

                return stored;
            }
        }
    }
}
