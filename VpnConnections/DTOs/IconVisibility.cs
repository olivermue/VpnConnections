using VpnConnections.Localization;

namespace VpnConnections.DTOs
{
    public enum IconVisibility
    {
        [DescriptionLocalized("Always")]
        Always,

        [DescriptionLocalized("Only connected")]
        OnlyConnected,

        [DescriptionLocalized("Only disconnected")]
        OnlyDisconnected,

        [DescriptionLocalized("Never")]
        Never,
    }
}