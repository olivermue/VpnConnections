using System.Runtime.InteropServices;

namespace VpnConnections.Ras
{
    internal delegate void RasDialFunction(int param1, RasConnectionState rasConnectionState, int param3);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public struct RasDialParams
    {
        public int dwSize = Marshal.SizeOf<RasDialParams>();

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
        public string szEntryName = string.Empty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string szPhoneNumber = string.Empty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string szCallbackNumber = string.Empty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
        public string szUserName = string.Empty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
        public string szPassword = string.Empty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szDomain = string.Empty;

        public int dwSubEntry;
        public IntPtr dwCallbackId;
        public int dwIfIndex;

        public RasDialParams()
        {
        }
    }
}