using System.Diagnostics.CodeAnalysis;

namespace VpnConnections.DTOs
{
    public class VpnConnectionSettingsEqualityComparer : IEqualityComparer<VpnConnectionSettings>
    {
        public bool Equals(VpnConnectionSettings? x, VpnConnectionSettings? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null
                || y is null)
                return false;

            return x.ConnectionName == y.ConnectionName
                && x.ShowNotification == y.ShowNotification
                && x.AccentColorForConnected == y.AccentColorForConnected
                && x.TrayIconConnectedColor == y.TrayIconConnectedColor
                && x.AccentColorForDisonnected == y.AccentColorForDisonnected
                && x.TrayIconDisconnectedColor == y.TrayIconDisconnectedColor
                && x.TrayIconLeftMouseButtonClick == y.TrayIconLeftMouseButtonClick
                && x.TrayIconLeftMouseButtonDoubleClick == y.TrayIconLeftMouseButtonDoubleClick
                && x.TrayIconRightMouseButtonClick == y.TrayIconRightMouseButtonClick
                && x.TrayIconRightMouseButtonDoubleClick == y.TrayIconRightMouseButtonDoubleClick
                && x.TrayIconVisibility == y.TrayIconVisibility
                && x.RunOnStartup == y.RunOnStartup;
        }

        public int GetHashCode([DisallowNull] VpnConnectionSettings obj)
        {
            var hash = new HashCode();
            hash.Add(obj.ConnectionName);
            hash.Add(obj.ShowNotification);
            hash.Add(obj.AccentColorForConnected);
            hash.Add(obj.TrayIconConnectedColor);
            hash.Add(obj.AccentColorForDisonnected);
            hash.Add(obj.TrayIconDisconnectedColor);
            hash.Add(obj.TrayIconLeftMouseButtonClick);
            hash.Add(obj.TrayIconLeftMouseButtonDoubleClick);
            hash.Add(obj.TrayIconRightMouseButtonClick);
            hash.Add(obj.TrayIconRightMouseButtonDoubleClick);
            hash.Add(obj.TrayIconVisibility);
            hash.Add(obj.RunOnStartup);

            return hash.ToHashCode();
        }
    }
}