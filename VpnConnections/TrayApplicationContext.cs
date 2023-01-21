using System.Text.Json;
using System.Text.Json.Serialization;
using VpnConnections.Dialogs;
using VpnConnections.Drawing;
using VpnConnections.DTOs;
using VpnConnections.Helpers;
using VpnConnections.Vpn;
using Windows.UI.ViewManagement;
using Timer = System.Windows.Forms.Timer;

namespace VpnConnections
{
    public class TrayApplicationContext : ApplicationContext
    {
        private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
        };

        private readonly ConfigurationDialog configurationDialog;
        private readonly FileSystemWatcher fileWatcher;
        private readonly Timer indicator;
        private readonly NotifyIcon notifyIcon;
        private readonly Timer throttle;
        private readonly UISettings uiSettings;
        private readonly VpnConnection vpnConnection;

        private string? latestAccentColor;
        private VpnConnectionSettings settings;
        private Icon statusActiveIcon;
        private Icon statusConnectingIcon;
        private Icon statusInactiveIcon;
        private Action? throttledExecution;

        public TrayApplicationContext()
        {
            ApplyCulture();

            throttle = new Timer
            {
                Interval = 500,
                Enabled = false,
            };
            throttle.Tick += OnThrottleTick;

            indicator = new Timer
            {
                Interval = 250,
                Enabled = false,
            };
            indicator.Tick += OnIndicatorTick;

            notifyIcon = new NotifyIcon
            {
                Text = Properties.Resources.MessageDefault,
                Visible = true,
            };
            notifyIcon.Click += OnIconClick;
            notifyIcon.DoubleClick += OnIconClick;
            notifyIcon.MouseMove += OnIconHover;

            configurationDialog = new ConfigurationDialog();
            configurationDialog.SettingsChanged += OnSettingsChanged;
            configurationDialog.QuitClicked += OnQuitClicked;
            var _ = configurationDialog.Handle;

            vpnConnection = new VpnConnection();
            vpnConnection.IsConnectedChanged += OnVpnIsConnectedChanged;
            vpnConnection.ConnectionStateChanged += OnVpnConnectionStateChanged;

            fileWatcher = new FileSystemWatcher
            {
                Filter = GetSettingsFilePath(),
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.Size,
                Path = Directory.GetCurrentDirectory(),
            };
            fileWatcher.Changed += OnWatcherChanged;
            fileWatcher.Created += OnWatcherChanged;
            fileWatcher.EnableRaisingEvents = true;

            uiSettings = new UISettings();
            latestAccentColor = GetAccentColor();
            uiSettings.ColorValuesChanged += OnUiColorValuesChanged;

            settings = new VpnConnectionSettings();
            statusActiveIcon = Properties.Resources.StatusActive;
            statusInactiveIcon = Properties.Resources.StatusActive;
            statusConnectingIcon = Properties.Resources.StatusActive;
            ApplySettings();
            CheckVisibility();
        }

        protected override void Dispose(bool disposing)
        {
            vpnConnection.Dispose();
            fileWatcher.Dispose();
            base.Dispose(disposing);
        }

        protected override void ExitThreadCore()
        {
            notifyIcon.Visible = false;
            base.ExitThreadCore();
        }

        private static void ApplyCulture()
        {
            var value = CommandLine.GetArgumentValue("culture");

            if (!string.IsNullOrWhiteSpace(value))
            {
                Cultures.SetCulture(value);
            }
        }

        private static Icon CreateConnectingIcon(string? colorValue)
        {
            return Icons.CreateIcon(colorValue, Properties.Resources.ConnectingForeground, Properties.Resources.ConnectingBackground);
        }

        private static Icon CreateStatusIcon(string? colorValue)
        {
            return Icons.CreateIcon(colorValue, Properties.Resources.StatusForeground, Properties.Resources.StatusBackground);
        }

        private static string GetSettingsFilePath()
        {
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var defaultAppFile = Path.Combine(appFolder, Application.ProductName, Properties.Resources.SettingsFilename);

            // Check app data folder
            if (File.Exists(defaultAppFile))
                return defaultAppFile;

            // Check working directory
            if (File.Exists(Properties.Resources.SettingsFilename))
                return Properties.Resources.SettingsFilename;

            var directory = Path.GetDirectoryName(Application.ExecutablePath);
            var appFile = Path.Combine(directory!, Properties.Resources.SettingsFilename);

            // Check app folder
            if (File.Exists(appFile))
                return appFile;

            // None found, prepare app data folder for watcher
            Directory.CreateDirectory(Path.GetDirectoryName(defaultAppFile)!);

            return defaultAppFile;
        }

        private void ApplySettings()
        {
            var settingsFilename = GetSettingsFilePath();
            var appSettings = !File.Exists(settingsFilename)
                ? "{}"
                : Multiple.Try(3, TimeSpan.FromMilliseconds(250),
                    () => File.ReadAllText(settingsFilename),
                    "{}");

            settings = JsonSerializer.Deserialize<VpnConnectionSettings>(appSettings, serializerOptions)!;

            var connectedColor = settings.AccentColorForConnected
                ? GetAccentColor()
                : settings.TrayIconConnectedColor;

            var disconnectedColor = settings.AccentColorForDisonnected
                ? GetAccentColor()
                : settings.TrayIconDisconnectedColor;

            vpnConnection.ObservedConnectionName = settings.ConnectionName;
            statusActiveIcon = CreateStatusIcon(connectedColor);
            statusInactiveIcon = CreateStatusIcon(disconnectedColor);
            statusConnectingIcon = CreateConnectingIcon(connectedColor);

            Autostart.CurrentUser.Set(settings.RunOnStartup);
            UpdateUI();
        }

        private void CheckVisibility()
        {
            var showDialog = CommandLine.HasArgument("show");

            var dialogNotReachable = settings.TrayIconVisibility == IconVisibility.Never
                || !settings.ActionConfigured(ClickAction.OpenConfiguration);

            if (showDialog
                || dialogNotReachable)
            {
                throttledExecution = configurationDialog.Show;
                throttle.Start();
            }
        }

        private string CreateMessage()
        {
            return vpnConnection.IsConnected
                ? string.Format(Properties.Resources.MessageConnected, vpnConnection.ObservedConnectionName)
                : string.Format(Properties.Resources.MessageDisconnected, vpnConnection.ObservedConnectionName);
        }

        private void Execute(ClickAction clickAction)
        {
            switch (clickAction)
            {
                case ClickAction.ConnectOnly:
                    if (!vpnConnection.IsConnected)
                        vpnConnection.Connect();
                    break;

                case ClickAction.DisconnectOnly:
                    if (vpnConnection.IsConnected)
                        vpnConnection.Disconnect();
                    break;

                case ClickAction.ToggleConnectionState:
                    if (vpnConnection.IsConnected)
                        vpnConnection.Disconnect();
                    else
                        vpnConnection.Connect();
                    break;

                //case ClickAction.OpenConnectionDialog:
                //    // ToDo: Tell explorer to show taskbar VPN connections dialog
                //    break;

                case ClickAction.OpenConfiguration:
                    configurationDialog.Show();
                    break;

                case ClickAction.CloseApplication:
                    Application.Exit();
                    break;
            }
        }

        private string? GetAccentColor()
        {
            var color = uiSettings.GetColorValue(UIColorType.Accent);
            return Design.ColorConverter.From(Design.ColorConverter.From(color));
        }

        private void OnIconClick(object? sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs ?? new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
            throttledExecution = () => OnIconMouseClick(sender, mouseEventArgs);
            throttle.Start();
        }

        private void OnIconHover(object? sender, MouseEventArgs e)
        {
            var message = CreateMessage();
            var duration = vpnConnection.Duration;

            if (duration > TimeSpan.Zero)
            {
                message += duration.Days > 0
                    ? $" {duration:d\\.hh\\:mm\\:ss}"
                    : $" {duration:hh\\:mm\\:ss}";
            }

            notifyIcon.Text = message;
        }

        private void OnIconMouseClick(object? sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    switch (e.Clicks)
                    {
                        case 0:
                        case 1:
                            Execute(settings.TrayIconLeftMouseButtonClick);
                            break;

                        default:
                            Execute(settings.TrayIconLeftMouseButtonDoubleClick);
                            break;
                    }
                    break;

                case MouseButtons.Right:
                    switch (e.Clicks)
                    {
                        case 0:
                        case 1:
                            Execute(settings.TrayIconRightMouseButtonClick);
                            break;

                        default:
                            Execute(settings.TrayIconRightMouseButtonDoubleClick);
                            break;
                    }
                    break;
            }
        }

        private void OnIndicatorTick(object? sender, EventArgs e)
        {
            notifyIcon.Icon = notifyIcon.Icon == statusInactiveIcon
                ? statusConnectingIcon
                : statusInactiveIcon;
        }

        private void OnQuitClicked(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSettingsChanged(object? sender, EventArgs e)
        {
            var json = JsonSerializer.Serialize(configurationDialog.Settings, serializerOptions);
            File.WriteAllText(GetSettingsFilePath(), json);
        }

        private void OnThrottleTick(object? sender, EventArgs e)
        {
            throttle.Stop();
            throttledExecution?.Invoke();
            throttledExecution = null;
        }

        private void OnUiColorValuesChanged(UISettings sender, object args)
        {
            var currentAccentColor = GetAccentColor();

            if (latestAccentColor != currentAccentColor)
            {
                ApplySettings();
                latestAccentColor = currentAccentColor;
            }
        }

        private void OnVpnConnectionStateChanged(object? sender, EventArgs e)
        {
            if (vpnConnection.ConnectionState == ConnectionState.Connecting)
                configurationDialog.BeginInvoke(indicator.Start);
            else
                configurationDialog.BeginInvoke(indicator.Stop);
        }

        private void OnVpnIsConnectedChanged(object? sender, EventArgs e)
        {
            UpdateUI();
        }

        private void OnWatcherChanged(object? sender, FileSystemEventArgs e)
        {
            ApplySettings();
        }

        private void ShowNotificationMessage()
        {
            notifyIcon.BalloonTipText = CreateMessage();
            notifyIcon.ShowBalloonTip(0);
        }

        private void UpdateEditor()
        {
            configurationDialog.Settings = settings;
            configurationDialog.Icon = vpnConnection.IsConnected
                ? statusActiveIcon
                : statusInactiveIcon;
        }

        private void UpdateUI()
        {
            // Disable visibility to remove old message notifications.
            notifyIcon.Visible = false;

            var isConnected = vpnConnection.IsConnected;
            notifyIcon.Visible = settings.TrayIconVisibility switch
            {
                IconVisibility.OnlyConnected => isConnected,
                IconVisibility.OnlyDisconnected => !isConnected,
                IconVisibility.Never => false,
                _ => true,
            };

            notifyIcon.Text = CreateMessage();
            notifyIcon.Icon = isConnected
                ? statusActiveIcon
                : statusInactiveIcon;

            configurationDialog.BeginInvoke(UpdateEditor);

            switch (settings.ShowNotification)
            {
                case ShowNotification.OnConnectionChange:
                    ShowNotificationMessage();
                    break;

                case ShowNotification.OnConnectedOnly:
                    if (isConnected)
                        ShowNotificationMessage();
                    break;

                case ShowNotification.OnDisconnectedOnly:
                    if (!isConnected)
                        ShowNotificationMessage();
                    break;
            }
        }
    }
}