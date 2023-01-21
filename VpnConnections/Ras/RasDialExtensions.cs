using System.Runtime.InteropServices;

namespace VpnConnections.Ras
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RasDialExtensions
    {
        public int dwSize = Marshal.SizeOf<RasDialExtensions>();
        public int dwfOptions;
        public IntPtr hwndParent;
        public IntPtr reserved;
        public IntPtr reserved1;
        public RasEapInfo RasEapInfo;
        public bool fSkipPppAuth;
        public RasDevSpecificInfo RasDevSpecificInfo;

        public RasDialExtensions()
        {
        }
    }
}