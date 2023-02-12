using Microsoft.Win32;
using VpnConnections.Logs;

namespace VpnConnections.Helpers
{
    public static class Autostart
    {
        public static class CurrentUser
        {
            public static void Set(bool enable)
            {
                var rk = Registry.CurrentUser.OpenSubKey(Properties.Resources.RunKey, true);
                Logging.LogInfo($"Set app autostart to {enable}", "Autostart.CurrentUser");

                if (enable)
                    rk?.SetValue(Application.ProductName, Application.ExecutablePath);
                else
                    rk?.DeleteValue(Application.ProductName, false);
            }
        }
    }
}