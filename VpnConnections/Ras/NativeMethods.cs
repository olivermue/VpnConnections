using System.Runtime.InteropServices;

namespace VpnConnections.Ras
{
    internal static class NativeMethods
    {
        [DllImport("rasapi32.dll", CharSet = CharSet.Unicode)]
        public static extern RasError RasDial(
            [In] ref RasDialExtensions lpRasDialExtensions,
            string lpszPhoneBook,
            [In] ref RasDialParams lpRasDialParams,
            NotifierType dwNotifierType,
            RasDialFunction lpvNotifier,
            out IntPtr lphRasConn);

        [DllImport("rasapi32.dll", CharSet = CharSet.Unicode)]
        public static extern RasError RasGetEntryDialParams(
        string lpszPhoneBook,
        [In, Out] ref RasDialParams lpDialParams,
        [Out] out bool lpfPassword);

        [DllImport("rasapi32.dll", CharSet = CharSet.Unicode)]
        public static extern RasError RasHangUp(IntPtr hRasConn);
    }
}