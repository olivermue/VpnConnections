using System.ComponentModel;
using System.Net.NetworkInformation;
using NETWORKLIST;
using VpnConnections.Helpers;
using VpnConnections.Logs;
using VpnConnections.Ras;
using Timer = System.Timers.Timer;

namespace VpnConnections.Vpn
{
    public class VpnConnection : INotifyPropertyChanged, IDisposable
    {
        private static readonly PropertyChangedEventArgs ConnectionStatePropertyChangedArgs = new PropertyChangedEventArgs(nameof(ConnectionState));
        private static readonly PropertyChangedEventArgs IsConnectedPropertyChangedArgs = new PropertyChangedEventArgs(nameof(IsConnected));
        private static readonly Logger logger = new Logger(nameof(VpnConnection));
        private static readonly Logger rasLogger = new Logger("RasDial");

        private readonly NetworkListManager networkListManager;
        private readonly Timer timer;

        private IntPtr connectionHandle = IntPtr.Zero;
        private bool firstRun;
        private NetworkInterface? interfaceAvailable;

        public VpnConnection()
        {
            firstRun = true;
            networkListManager = new NetworkListManager();
            timer = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 2000,
            };

            timer.Elapsed += (_, __) => UpdateNetworkState();
        }

        public event EventHandler? ConnectionStateChanged;

        public event EventHandler? IsConnectedChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        public static IReadOnlyList<string> ConnectionNames
        {
            get { return ReadConnectionNames(); }
        }

        public ConnectionState ConnectionState { get; private set; }
        public TimeSpan Duration => GetDuration(interfaceAvailable);

        public bool IsConnected { get; private set; }
        public string? ObservedConnectionName { get; set; }

        public void Connect()
        {
            var phonebook = PhonebookLocation();
            var dialParams = new RasDialParams
            {
                szEntryName = ObservedConnectionName ?? string.Empty,
            };

            var result = NativeMethods.RasGetEntryDialParams(phonebook, ref dialParams, out _);

            if (result != 0)
            {
                rasLogger.LogError($"Failed to get entry for {dialParams.szEntryName}: {result}");
                return;
            }

            var extension = new RasDialExtensions();

            for (int i = 0; i < 3; i++)
            {
                connectionHandle = IntPtr.Zero;
                result = NativeMethods.RasDial(ref extension, phonebook, ref dialParams, NotifierType.RasDialFunc, OnRasDial, out connectionHandle);
                rasLogger.LogInfo($"Dial {dialParams.szEntryName} got handle {connectionHandle} and result {result}");

                if (result == RasError.NO_ERROR)
                    break;

                DisconnectForced();
            }
        }

        public void Disconnect()
        {
            if (connectionHandle == IntPtr.Zero
                && IsConnected)
            {
                // Call connect to retrieve handle.
                // Doesn't do anything else, cause connection is already up and running.
                Connect();
            }

            DisconnectForced();
        }

        public void Dispose()
        {
            timer.Dispose();
            GC.SuppressFinalize(this);
        }

        private static async Task<IReadOnlyList<NetworkInterface>> GetNetworkInterfaces()
        {
            return await Multiple.TryAsync(
                3,
                TimeSpan.FromMilliseconds(100),
                NetworkInterface.GetAllNetworkInterfaces,
                Array.Empty<NetworkInterface>());
        }

        private static string PhonebookLocation()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Resources.DefaultPhonebook);
        }

        private static IReadOnlyList<string> ReadConnectionNames()
        {
            var file = PhonebookLocation();
            var lines = File.ReadLines(file);

            var connections = lines
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Trim())
                .Where(line => line.StartsWith("[") && line.EndsWith("]"))
                .Select(line => line[1..^1])
                .ToList();

            logger.LogInfo($"Connections found: {string.Join(", ", connections)}");

            return connections;
        }

        private void DisconnectForced()
        {
            if (connectionHandle != IntPtr.Zero)
            {
                RasError result;

                do
                {
                    result = NativeMethods.RasHangUp(connectionHandle);
                    rasLogger.LogInfo($"HangUp handle {connectionHandle}: {result}");
                } while (result == RasError.NO_ERROR);

                connectionHandle = IntPtr.Zero;
            }
        }

        private TimeSpan GetDuration(NetworkInterface? networkInterface)
        {
            if (networkInterface == null)
                return TimeSpan.Zero;

            var networks = networkListManager.GetNetworks(NLM_ENUM_NETWORK.NLM_ENUM_NETWORK_CONNECTED).Cast<INetwork>();
            var networkFound = networks.FirstOrDefault(network => network.GetName() == networkInterface.Name);

            if (networkFound == null)
                return TimeSpan.Zero;

            networkFound.GetTimeCreatedAndConnected(out uint _, out uint _, out uint pdwLowDateTimeConnected, out uint pdwHighDateTimeConnected);
            var networkConnectedTime = DateTime.FromFileTimeUtc((long)((ulong)pdwHighDateTimeConnected << 32 | pdwLowDateTimeConnected));
            networkConnectedTime = DateTime.SpecifyKind(networkConnectedTime, DateTimeKind.Local);

            var duration = DateTime.Now.Subtract(networkConnectedTime);
            logger.LogInfo($"Calculated duration: {duration}");

            return duration;
        }

        private void OnRasDial(int _, RasConnectionState rasConnectionState, int __)
        {
            rasLogger.LogInfo($"Callback: {rasConnectionState}");

            var newState = rasConnectionState switch
            {
                RasConnectionState.Connected => ConnectionState.Connected,
                RasConnectionState.Disconnected => ConnectionState.Disconnected,
                _ => ConnectionState.Connecting,
            };

            if (ConnectionState != newState)
            {
                logger.LogInfo($"Update connection state: {newState}");
                ConnectionState = newState;
                PropertyChanged?.Invoke(this, ConnectionStatePropertyChangedArgs);
                ConnectionStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void UpdateNetworkState()
        {
            var connectionName = ObservedConnectionName;

            if (!string.IsNullOrEmpty(connectionName))
            {
                var allInterfaces = await GetNetworkInterfaces();
                interfaceAvailable = allInterfaces
                    .Where(network => network.Name == connectionName)
                    .Where(network => network.OperationalStatus == OperationalStatus.Up)
                    .FirstOrDefault();

                var hasConnection = interfaceAvailable != null;
                Logging.LogInfo($"{connectionName} connected: {hasConnection}", "Timer");

                if (firstRun || IsConnected != hasConnection)
                {
                    logger.LogInfo($"Changed network state: {hasConnection}");

                    firstRun = false;
                    IsConnected = hasConnection;
                    PropertyChanged?.Invoke(this, IsConnectedPropertyChangedArgs);
                    IsConnectedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}