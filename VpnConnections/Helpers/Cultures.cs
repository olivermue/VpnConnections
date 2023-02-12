using System.Globalization;
using VpnConnections.Logs;

namespace VpnConnections.Helpers
{
    public static class Cultures
    {
        public static void SetCulture(string cultureName)
        {
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                var cultureFound = CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(culture =>
                        StringComparer.OrdinalIgnoreCase.Equals(culture.Name, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.DisplayName, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.EnglishName, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.NativeName, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.IetfLanguageTag, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.TwoLetterISOLanguageName, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.ThreeLetterISOLanguageName, cultureName)
                        || StringComparer.OrdinalIgnoreCase.Equals(culture.ThreeLetterWindowsLanguageName, cultureName));

                if (cultureFound != null)
                {
                    Logging.LogInfo($"Switch to culture {cultureFound.Name}");
                    Thread.CurrentThread.CurrentCulture = cultureFound;
                    Thread.CurrentThread.CurrentUICulture = cultureFound;
                }
            }
        }
    }
}