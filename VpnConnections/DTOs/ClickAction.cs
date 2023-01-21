using System.ComponentModel;
using VpnConnections.Localization;

namespace VpnConnections.DTOs
{
    public enum ClickAction
    {
        [DescriptionLocalized("Open configuration dialog")]
        OpenConfiguration,

        [DescriptionLocalized("Connect only")]
        ConnectOnly,

        [DescriptionLocalized("Disconnect only")]
        DisconnectOnly,

        [DescriptionLocalized("Toggle connection state")]
        ToggleConnectionState,

        //// ToDo: Tell explorer to show taskbar VPN connections dialog
        //[DescriptionLocalized("Open connections")]
        //OpenConnectionDialog,

        [DescriptionLocalized("Close application")]
        CloseApplication,
    }
}