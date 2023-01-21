namespace VpnConnections.DTOs
{
    public class VpnConnectionSettings
    {
        public string ConnectionName { get; set; } = string.Empty;
        public ShowNotification ShowNotification { get; set; }
        public bool AccentColorForConnected { get; set; }
        public string? TrayIconConnectedColor { get; set; } = "128, 255, 128";
        public bool AccentColorForDisonnected { get; set; }
        public string? TrayIconDisconnectedColor { get; set; } = "255, 128, 128";
        public ClickAction TrayIconLeftMouseButtonClick { get; set; } = ClickAction.OpenConfiguration;
        public ClickAction TrayIconLeftMouseButtonDoubleClick { get; set; } = ClickAction.OpenConfiguration;
        public ClickAction TrayIconRightMouseButtonClick { get; set; } = ClickAction.OpenConfiguration;
        public ClickAction TrayIconRightMouseButtonDoubleClick { get; set; } = ClickAction.CloseApplication;
        public IconVisibility TrayIconVisibility { get; set; }
        public bool RunOnStartup { get; set; }

        public bool ActionConfigured(ClickAction clickAction)
        {
            return TrayIconLeftMouseButtonClick == clickAction
                || TrayIconLeftMouseButtonDoubleClick == clickAction
                || TrayIconRightMouseButtonClick == clickAction
                || TrayIconRightMouseButtonDoubleClick == clickAction;
        }
    }
}