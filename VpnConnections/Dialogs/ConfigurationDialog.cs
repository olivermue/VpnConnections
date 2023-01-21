using System.ComponentModel;
using VpnConnections.Design;
using VpnConnections.DTOs;

namespace VpnConnections.Dialogs
{
    public partial class ConfigurationDialog : Form
    {
        private VpnConnectionSettings settings = new VpnConnectionSettings();

        public ConfigurationDialog()
        {
            InitializeComponent();
        }

        public event EventHandler? QuitClicked;

        public event EventHandler? SettingsChanged;

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
            Visible = false;
        }

        private void OnButtonQuitClick(object sender, EventArgs e)
        {
            QuitClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnButtonSaveClick(object sender, EventArgs e)
        {
            var editorSettings = (EditorSettings)propertyGrid.SelectedObject;
            settings = editorSettings.CreateVpnConnectionSettings();

            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}