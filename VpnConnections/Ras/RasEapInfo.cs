using System.Runtime.InteropServices;

namespace VpnConnections.Ras
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RasEapInfo
    {
        public int dwSizeofEapInfo = Marshal.SizeOf<RasEapInfo>();
        public IntPtr pbEapInfo;

        public RasEapInfo()
        {
        }
    }
}