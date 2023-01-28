using System.Diagnostics;
using VpnConnections.Logging;

namespace VpnConnections.Localization
{
    public static class Localize
    {
        public static string Value(string name)
        {
            var localized = Properties.Resources.ResourceManager.GetString(name);

            if (localized is null)
                Logger.LogWarning($"Needed translation: {name}");

            return localized
                ?? name;
        }
    }
}