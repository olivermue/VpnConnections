using System.ComponentModel;
using System.Net.NetworkInformation;
using NETWORKLIST;
using VpnConnections.Helpers;
using VpnConnections.Ras;
using Timer = System.Timers.Timer;

namespace VpnConnections.Vpn
{
    public class VpnConnection : INotifyPropertyChanged, IDisposable
    {
        private static readonly PropertyChangedEventArgs ConnectionStatePropertyChangedArgs = new PropertyChangedEventArgs(nameof(ConnectionState));
        private static readonly PropertyChangedEventArgs IsConnectedPropertyChangedArgs = new PropertyChangedEventArgs(nameof(IsConnected));

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
                return;

            var extension = new RasDialExtensions();

            result = NativeMethods.RasDial(ref extension, phonebook, ref dialParams, NotifierType.RasDialFunc, OnRasDial, out connectionHandle);
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

            if (connectionHandle != IntPtr.Zero)
            {
                int result;

                do
                {
                    result = NativeMethods.RasHangUp(connectionHandle);
                } while (result == 0);

                connectionHandle = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            ((IDisposable)timer).Dispose();
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

            return lines
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Trim())
                .Where(line => line.StartsWith("["))
                .Where(line => line.EndsWith("]"))
                .Select(line => line[1..^1])
                .ToList();
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

            return DateTime.Now.Subtract(networkConnectedTime);
        }

        private void OnRasDial(int _, RasConnectionState rasConnectionState, int __)
        {
            var newState = rasConnectionState switch
            {
                RasConnectionState.Connected => ConnectionState.Connected,
                RasConnectionState.Disconnected => ConnectionState.Disconnected,
                _ => ConnectionState.Connecting,
            };

            if (ConnectionState != newState)
            {
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

                if (firstRun || IsConnected != hasConnection)
                {
                    firstRun = false;
                    IsConnected = hasConnection;
                    PropertyChanged?.Invoke(this, IsConnectedPropertyChangedArgs);
                    IsConnectedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}