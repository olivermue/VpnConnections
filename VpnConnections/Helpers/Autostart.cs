using Microsoft.Win32;
using VpnConnections.Logging;

namespace VpnConnections.Helpers
{
    public static class Autostart
    {
        public static class CurrentUser
        {
            public static void Set(bool enable)
            {
                var rk = Registry.CurrentUser.OpenSubKey(Properties.Resources.RunKey, true);
                Logger.LogInfo($"Set app autostart to {enable}");

                if (enable)
                    rk?.SetValue(Application.ProductName, Application.ExecutablePath);
                else
                    rk?.DeleteValue(Application.ProductName, false);
            }
        }
    }
}