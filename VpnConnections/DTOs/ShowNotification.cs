using VpnConnections.Localization;

namespace VpnConnections.DTOs
{
    public enum ShowNotification
    {
        [DescriptionLocalized("Never")]
        Never,

        [DescriptionLocalized("On connection change")]
        OnConnectionChange,

        [DescriptionLocalized("On connected only")]
        OnConnectedOnly,

        [DescriptionLocalized("On disconnected only")]
        OnDisconnectedOnly,
    }
}