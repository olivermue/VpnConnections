using System.Runtime.InteropServices;

namespace VpnConnections.Ras
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RasDevSpecificInfo
    {
        public int dwSize = Marshal.SizeOf<RasDevSpecificInfo>();
        public IntPtr pbDevSpecificInfo;

        public RasDevSpecificInfo()
        {
        }
    }
}