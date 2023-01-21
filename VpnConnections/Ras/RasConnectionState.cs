﻿namespace VpnConnections.Ras
{
    public enum RasConnectionState
    {
        OpenPort,
        PortOpened,
        ConnectDevice,
        DeviceConnected,
        AllDevicesConnected,
        Authenticate,
        AuthNotify,
        AuthRetry,
        AuthCallback,
        AuthChangePassword,
        AuthProject,
        AuthLinkSpeed,
        AuthAck,
        ReAuthenticate,
        Authenticated,
        PrepareForCallback,
        WaitForModemReset,
        WaitForCallback,
        Projected,
        StartAuthentication,
        CallbackComplete,
        LogOnNetwork,
        SubEntryConnected,
        SubEntryDisconnected,
        ApplySettings,
        Interactive = 0x1000,
        RetryAuthentication,
        CallbackSetByCaller,
        PasswordExpired,
        InvokeEapUI,
        Connected = 0x2000,
        Disconnected,
    }
}