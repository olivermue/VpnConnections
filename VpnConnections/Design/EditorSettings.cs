using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using VpnConnections.DTOs;
using VpnConnections.Localization;

namespace VpnConnections.Design
{
    public class EditorSettings
    {
        private bool accentColorForConnected;
        private bool accentColorForDisonnected;

        [TypeConverter(typeof(ConnectionNameConverter))]
        [CategoryLocalized("Connection")]
        [DisplayNameLocalized("Connection Name")]
        public string ConnectionName { get; set; } = string.Empty;

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Tray icon visibility")]
        public IconVisibility TrayIconVisibility { get; set; }

        [TypeConverter(typeof(BooleanLocalizeConverter))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Accent color for connected")]
        public bool AccentColorForConnected
        {
            get => accentColorForConnected;
            set
            {
                accentColorForConnected = value;
                UpdatePropertyReadOnly(nameof(TrayIconConnectedColor), value);
            }
        }

        [TypeConverter(typeof(ColorConverter))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Tray icon connected color")]
        [ReadOnly(false)]
        [RefreshProperties(RefreshProperties.All)]
        public Color TrayIconConnectedColor { get; set; } = Color.GreenYellow;

        [TypeConverter(typeof(BooleanLocalizeConverter))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Accent color for disconnected")]
        public bool AccentColorForDisonnected
        {
            get => accentColorForDisonnected;
            set
            {
                accentColorForDisonnected = value;
                UpdatePropertyReadOnly(nameof(TrayIconDisconnectedColor), value);
            }
        }

        [TypeConverter(typeof(ColorConverter))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Tray icon disconnected color")]
        [ReadOnly(false)]
        [RefreshProperties(RefreshProperties.All)]
        public Color TrayIconDisconnectedColor { get; set; } = Color.Transparent;

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Appearance")]
        [DisplayNameLocalized("Show notification")]
        public ShowNotification ShowNotification { get; set; }

        [TypeConverter(typeof(BooleanLocalizeConverter))]
        [CategoryLocalized("Behaviour")]
        [DisplayNameLocalized("Run tool on startup")]
        public bool RunOnStartup { get; set; }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Behaviour")]
        [DisplayNameLocalized("Tray icon left mouse button click")]
        public ClickAction TrayIconLeftMouseButtonClick { get; set; }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Behaviour")]
        [DisplayNameLocalized("Tray icon left mouse button double click")]
        public ClickAction TrayIconLeftMouseButtonDoubleClick { get; set; }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Behaviour")]
        [DisplayNameLocalized("Tray icon right mouse button click")]
        public ClickAction TrayIconRightMouseButtonClick { get; set; }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [CategoryLocalized("Behaviour")]
        [DisplayNameLocalized("Tray icon right mouse button double click")]
        public ClickAction TrayIconRightMouseButtonDoubleClick { get; set; }

        public static EditorSettings From(VpnConnectionSettings source) => new EditorSettings
        {
            ConnectionName = source.ConnectionName,
            RunOnStartup = source.RunOnStartup,
            ShowNotification = source.ShowNotification,
            TrayIconLeftMouseButtonClick = source.TrayIconLeftMouseButtonClick,
            TrayIconLeftMouseButtonDoubleClick = source.TrayIconLeftMouseButtonDoubleClick,
            TrayIconRightMouseButtonClick = source.TrayIconRightMouseButtonClick,
            TrayIconRightMouseButtonDoubleClick = source.TrayIconRightMouseButtonDoubleClick,
            TrayIconVisibility = source.TrayIconVisibility,
            AccentColorForConnected = source.AccentColorForConnected,
            TrayIconConnectedColor = ColorConverter.From(source.TrayIconConnectedColor),
            AccentColorForDisonnected = source.AccentColorForDisonnected,
            TrayIconDisconnectedColor = ColorConverter.From(source.TrayIconDisconnectedColor),
        };

        public VpnConnectionSettings CreateVpnConnectionSettings()
        {
            return new VpnConnectionSettings
            {
                ConnectionName = ConnectionName,
                RunOnStartup = RunOnStartup,
                ShowNotification = ShowNotification,
                TrayIconLeftMouseButtonClick = TrayIconLeftMouseButtonClick,
                TrayIconLeftMouseButtonDoubleClick = TrayIconLeftMouseButtonDoubleClick,
                TrayIconRightMouseButtonClick = TrayIconRightMouseButtonClick,
                TrayIconRightMouseButtonDoubleClick = TrayIconRightMouseButtonDoubleClick,
                TrayIconVisibility = TrayIconVisibility,
                AccentColorForConnected = AccentColorForConnected,
                TrayIconConnectedColor = ColorConverter.From(TrayIconConnectedColor),
                AccentColorForDisonnected = AccentColorForDisonnected,
                TrayIconDisconnectedColor = ColorConverter.From(TrayIconDisconnectedColor),
            };
        }

        private void UpdatePropertyReadOnly(string propertyName, bool readOnly)
        {
            var prop = TypeDescriptor.GetProperties(GetType())[propertyName];
            var attribute = (ReadOnlyAttribute?)prop?.Attributes[typeof(ReadOnlyAttribute)];

            if (attribute != null)
            {
                var field = attribute.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(field => !string.IsNullOrEmpty(field.Name))
                    .FirstOrDefault(field => field.Name.Contains(nameof(ReadOnlyAttribute.IsReadOnly), StringComparison.OrdinalIgnoreCase));

                field?.SetValue(attribute, readOnly);
            }
        }
    }
}