using System.ComponentModel;
using VpnConnections.Design;
using VpnConnections.DTOs;
using VpnConnections.Logs;

namespace VpnConnections.Dialogs
{
    public partial class ConfigurationDialog : Form
    {
        private static readonly IEqualityComparer<VpnConnectionSettings> settingsComparer = new VpnConnectionSettingsEqualityComparer();
        private readonly Logger logger = new Logger(nameof(ConfigurationDialog));
        private VpnConnectionSettings settings = new VpnConnectionSettings();

        public ConfigurationDialog()
        {
            InitializeComponent();
        }

        public event EventHandler<ClickAction>? ActionRequested;

        public event EventHandler<bool>? SettingsChanged;

        public VpnConnectionSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                var editorSettings = EditorSettings.From(value);
                editorSettings.EnableLogging = Logging.Enabled;
                editorSettings.EnableLoggingChanged += OnEnabledLoggingChanged;
                propertyGrid.SelectedObject = editorSettings;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
            ApplySettingsIfChanged();
            Visible = false;
        }

        private void ApplySettingsIfChanged(bool enforceAppDataFolder = false)
        {
            var editorSettings = (EditorSettings)propertyGrid.SelectedObject;
            var newSettings = editorSettings.CreateVpnConnectionSettings();

            if (!settingsComparer.Equals(settings, newSettings)
                || enforceAppDataFolder)
            {
                logger.LogInfo("Inform about settings change");
                settings = newSettings;
                SettingsChanged?.Invoke(this, enforceAppDataFolder);
            }
        }

        private void OnButtonCloseClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button close clicked");
            ApplySettingsIfChanged();
            Visible = false;
        }

        private void OnButtonConnectClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button connect clicked");
            ActionRequested?.Invoke(this, ClickAction.ConnectOnly);
        }

        private void OnButtonDisconnectClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button disconnect clicked");
            ActionRequested?.Invoke(this, ClickAction.DisconnectOnly);
        }

        private void OnButtonQuitClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button quit clicked");
            ApplySettingsIfChanged();
            ActionRequested?.Invoke(this, ClickAction.CloseApplication);
        }

        private void OnButtonSaveClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button save clicked");
            ApplySettingsIfChanged();
        }

        private void OnButtonToggleClick(object sender, EventArgs e)
        {
            logger.LogInfo("Button toggle clicked");
            ActionRequested?.Invoke(this, ClickAction.ToggleConnectionState);
        }

        private void OnEnabledLoggingChanged(object? sender, EventArgs e)
        {
            var settings = (EditorSettings)sender!;
            Logging.Enabled = settings.EnableLogging;
        }
    }
}