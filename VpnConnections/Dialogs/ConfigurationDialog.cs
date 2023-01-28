using System.ComponentModel;
using VpnConnections.Design;
using VpnConnections.DTOs;

namespace VpnConnections.Dialogs
{
    public partial class ConfigurationDialog : Form
    {
        private static readonly IEqualityComparer<VpnConnectionSettings> settingsComparer = new VpnConnectionSettingsEqualityComparer();
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
                propertyGrid.SelectedObject = EditorSettings.From(value);
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
                settings = newSettings;
                SettingsChanged?.Invoke(this, enforceAppDataFolder);
            }
        }

        private void OnButtonCloseClick(object sender, EventArgs e)
        {
            ApplySettingsIfChanged();
            Visible = false;
        }

        private void OnButtonConnectClick(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, ClickAction.ConnectOnly);
        }

        private void OnButtonDisconnectClick(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, ClickAction.DisconnectOnly);
        }

        private void OnButtonQuitClick(object sender, EventArgs e)
        {
            ApplySettingsIfChanged();
            ActionRequested?.Invoke(this, ClickAction.CloseApplication);
        }

        private void OnButtonSaveClick(object sender, EventArgs e)
        {
            ApplySettingsIfChanged();
        }

        private void OnButtonToggleClick(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, ClickAction.ToggleConnectionState);
        }

        private void OnButtonSaveInAppDataClick(object sender, EventArgs e)
        {
            ApplySettingsIfChanged(true);
        }
    }
}