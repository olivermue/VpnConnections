using System.ComponentModel;

namespace VpnConnections.Ras
{
    public enum RasError
    {
        [Description("  0. No error")]
        NO_ERROR = 0,
        [Description("  6. Invalid handle")]
        ERROR_INVALID_HANDLE = 6,
        [Description(" 87. Invalid Parameter")]
        ERROR_INVALID_PARAMETER = 87,

        [Description("600. An operation is pending")]
        PENDING = 600,
        [Description("601. An invalid port handle was detected")]
        ERROR_INVALID_PORT_HANDLE,
        [Description("602. The specified port is already open")]
        ERROR_PORT_ALREADY_OPEN,
        [Description("603. The caller's buffer is too small")]
        ERROR_BUFFER_TOO_SMALL,
        [Description("604. Incorrect information was specified")]
        ERROR_WRONG_INFO_SPECIFIED,
        [Description("605. The port information cannot be set")]
        ERROR_CANNOT_SET_PORT_INFO,
        [Description("606. The specified port is not connected")]
        ERROR_PORT_NOT_CONNECTED,
        [Description("607. An invalid event was detected")]
        ERROR_EVENT_INVALID,
        [Description("608. A device was specified that does not exist")]
        ERROR_DEVICE_DOES_NOT_EXIST,
        [Description("609. A device type was specified that does not exist")]
        ERROR_DEVICETYPE_DOES_NOT_EXIST,
        [Description("610. An invalid buffer was specified")]
        ERROR_BUFFER_INVALID,
        [Description("611. A route was specified that is not available")]
        ERROR_ROUTE_NOT_AVAILABLE,
        [Description("612. A route was specified that is not allocated")]
        ERROR_ROUTE_NOT_ALLOCATED,
        [Description("613. An invalid compression was specified")]
        ERROR_INVALID_COMPRESSION_SPECIFIED,
        [Description("614. There were insufficient buffers available")]
        ERROR_OUT_OF_BUFFERS,
        [Description("615. The specified port was not found")]
        ERROR_PORT_NOT_FOUND,
        [Description("616. An asynchronous request is pending")]
        ERROR_ASYNC_REQUEST_PENDING,
        [Description("617. The modem (or other connecting device) is already disconnecting")]
        ERROR_ALREADY_DISCONNECTING,
        [Description("618. The specified port is not open")]
        ERROR_PORT_NOT_OPEN,
        [Description("619. The specified port is not connected")]
        ERROR_PORT_DISCONNECTED,
        [Description("620. No endpoints could be determined")]
        ERROR_NO_ENDPOINTS,
        [Description("621. The system could not open the phone book file")]
        ERROR_CANNOT_OPEN_PHONEBOOK,
        [Description("622. The system could not load the phone book file")]
        ERROR_CANNOT_LOAD_PHONEBOOK,
        [Description("623. The system could not find the phone book entry for this connection")]
        ERROR_CANNOT_FIND_PHONEBOOK_ENTRY,
        [Description("624. The system could not update the phone book file")]
        ERROR_CANNOT_WRITE_PHONEBOOK,
        [Description("625. The system found invalid information in the phone book file")]
        ERROR_CORRUPT_PHONEBOOK,
        [Description("626. A string could not be loaded")]
        ERROR_CANNOT_LOAD_STRING,
        [Description("627. A key could not be found")]
        ERROR_KEY_NOT_FOUND,
        [Description("628. The connection was closed")]
        ERROR_DISCONNECTION,
        [Description("629. The connection was closed by the remote computer")]
        ERROR_REMOTE_DISCONNECTION,
        [Description("630 The modem (or other connecting device) was disconnected due to hardware failure")]
        ERROR_HARDWARE_FAILURE,
        [Description("631. The user disconnected the modem (or other connecting device)")]
        ERROR_USER_DISCONNECTION,
        [Description("632. An incorrect structure size was detected")]
        ERROR_INVALID_SIZE,
        [Description("633. The modem (or other connecting device) is already in use or is not configured properly. ")]
        ERROR_PORT_NOT_AVAILABLE,
        [Description("634. Your computer could not be registered on the remote network")]
        ERROR_CANNOT_PROJECT_CLIENT,
        [Description("635. There was an unknown error")]
        ERROR_UNKNOWN,
        [Description("636. The device attached to the port is not the one expected")]
        ERROR_WRONG_DEVICE_ATTACHED,
        [Description("637. A string was detected that could not be converted")]
        ERROR_BAD_STRING,
        [Description("638. The request has timed out")]
        ERROR_REQUEST_TIMEOUT,
        [Description("639. No asynchronous net is available")]
        ERROR_CANNOT_GET_LANA,
        [Description("640. An error has occurred involving NetBIOS")]
        ERROR_NETBIOS_ERROR,
        [Description("641. The server cannot allocate NetBIOS resources needed to support the client")]
        ERROR_SERVER_OUT_OF_RESOURCES,
        [Description("642. One of your computer's NetBIOS names is already registered on the remote network")]
        ERROR_NAME_EXISTS_ON_NET,
        [Description("643. A network adapter at the server failed")]
        ERROR_SERVER_GENERAL_NET_FAILURE,
        [Description("644. You will not receive network message popups")]
        WARNING_MSG_ALIAS_NOT_ADDED,
        [Description("645. There was an internal authentication error")]
        ERROR_AUTH_INTERNAL,
        [Description("646. The account is not permitted to log on at this time of day")]
        ERROR_RESTRICTED_LOGON_HOURS,
        [Description("647. The account is disabled")]
        ERROR_ACCT_DISABLED,
        [Description("648. The password for this account has expired")]
        ERROR_PASSWD_EXPIRED,
        [Description("649. The account does not have permission to dial in")]
        ERROR_NO_DIALIN_PERMISSION,
        [Description("650. The remote access server is not responding")]
        ERROR_SERVER_NOT_RESPONDING,
        [Description("651. The modem (or other connecting device) has reported an error")]
        ERROR_FROM_DEVICE,
        [Description("652. There was an unrecognized response from the modem (or other connecting device)")]
        ERROR_UNRECOGNIZED_RESPONSE,
        [Description("653. A macro required by the modem (or other connecting device) was not found in the device.INF file")]
        ERROR_MACRO_NOT_FOUND,
        [Description("654. A command or response in the device.INF file section refers to an undefined macro")]
        ERROR_MACRO_NOT_DEFINED,
        [Description("655. The message macro was not found in the device.INF file section")]
        ERROR_MESSAGE_MACRO_NOT_FOUND,
        [Description("656. The defaultoff macro in the device.INF file section contains an undefined macro")]
        ERROR_DEFAULTOFF_MACRO_NOT_FOUND,
        [Description("657. The device.INF file could not be opened")]
        ERROR_FILE_COULD_NOT_BE_OPENED,
        [Description("658. The device name in the device.INF or media.INI file is too long")]
        ERROR_DEVICENAME_TOO_LONG,
        [Description("659. The media.INI file refers to an unknown device name")]
        ERROR_DEVICENAME_NOT_FOUND,
        [Description("660. The device.INF file contains no responses for the command")]
        ERROR_NO_RESPONSES,
        [Description("661. The device.INF file is missing a command")]
        ERROR_NO_COMMAND_FOUND,
        [Description("662. There was an attempt to set a macro not listed in device.INF file section")]
        ERROR_WRONG_KEY_SPECIFIED,
        [Description("663. The media.INI file refers to an unknown device type")]
        ERROR_UNKNOWN_DEVICE_TYPE,
        [Description("664. The system has run out of memory")]
        ERROR_ALLOCATING_MEMORY,
        [Description("665. The modem (or other connecting device) is not properly configured")]
        ERROR_PORT_NOT_CONFIGURED,
        [Description("666. The modem (or other connecting device) is not functioning")]
        ERROR_DEVICE_NOT_READY,
        [Description("667. The system was unable to read the media.INI file")]
        ERROR_READING_INI_FILE,
        [Description("668. The connection was terminated")]
        ERROR_NO_CONNECTION,
        [Description("669. The usage parameter in the media.INI file is invalid")]
        ERROR_BAD_USAGE_IN_INI_FILE,
        [Description("670. The system was unable to read the section name from the media.INI file")]
        ERROR_READING_SECTIONNAME,
        [Description("671. The system was unable to read the device type from the media.INI file")]
        ERROR_READING_DEVICETYPE,
        [Description("672. The system was unable to read the device name from the media.INI file")]
        ERROR_READING_DEVICENAME,
        [Description("673. The system was unable to read the usage from the media.INI file")]
        ERROR_READING_USAGE,
        [Description("674. The system was unable to read the maximum connection BPS rate from the media.INI file")]
        ERROR_READING_MAXCONNECTBPS,
        [Description("675. The system was unable to read the maximum carrier connection speed from the media.INI file")]
        ERROR_READING_MAXCARRIERBPS,
        [Description("676. The phone line is busy")]
        ERROR_LINE_BUSY,
        [Description("677. A person answered instead of a modem (or other connecting device)")]
        ERROR_VOICE_ANSWER,
        [Description("678. There was no answer")]
        ERROR_NO_ANSWER,
        [Description("679. The system could not detect the carrier")]
        ERROR_NO_CARRIER,
        [Description("680. There was no dial tone")]
        ERROR_NO_DIALTONE,
        [Description("681. The modem (or other connecting device) reported a general error")]
        ERROR_IN_COMMAND,
        [Description("682. There was an error in writing the section name")]
        ERROR_WRITING_SECTIONNAME,
        [Description("683. There was an error in writing the device type")]
        ERROR_WRITING_DEVICETYPE,
        [Description("684. There was an error in writing the device name")]
        ERROR_WRITING_DEVICENAME,
        [Description("685. There was an error in writing the maximum connection speed")]
        ERROR_WRITING_MAXCONNECTBPS,
        [Description("686. There was an error in writing the maximum carrier speed")]
        ERROR_WRITING_MAXCARRIERBPS,
        [Description("687. There was an error in writing the usage")]
        ERROR_WRITING_USAGE,
        [Description("688. There was an error in writing the default-off")]
        ERROR_WRITING_DEFAULTOFF,
        [Description("689. There was an error in reading the default-off")]
        ERROR_READING_DEFAULTOFF,
        [Description("690. ERROR EMPTY INI FILE")]
        ERROR_EMPTY_INI_FILE,
        /// <summary>691. Access was denied because the username and or password was invalid on the domain.</summary>
        ERROR_AUTHENTICATION_FAILURE,
        [Description("692. There was a hardware failure in the modem (or other connecting device)")]
        ERROR_PORT_OR_DEVICE,
        [Description("693. ERROR NOT BINARY MACRO")]
        ERROR_NOT_BINARY_MACRO,
        [Description("694. ERROR DCB NOT FOUND")]
        ERROR_DCB_NOT_FOUND,
        [Description("695. The state machines are not started")]
        ERROR_STATE_MACHINES_NOT_STARTED,
        [Description("696. The state machines are already started")]
        ERROR_STATE_MACHINES_ALREADY_STARTED,
        [Description("697. The response looping did not complete")]
        ERROR_PARTIAL_RESPONSE_LOOPING,
        [Description("698. A response keyname in the device.INF file is not in the expected format")]
        ERROR_UNKNOWN_RESPONSE_KEY,
        [Description("699. The modem (or other connecting device) response caused a buffer overflow")]
        ERROR_RECV_BUF_FULL,
        [Description("700. The expanded command in the device.INF file is too long")]
        ERROR_CMD_TOO_LONG,
        [Description("701. The modem moved to a connection speed not supported by the COM driver")]
        ERROR_UNSUPPORTED_BPS,
        [Description("702. Device response received when none expected")]
        ERROR_UNEXPECTED_RESPONSE,
        [Description("703. The connection needs information from you, but the application does not allow user interaction")]
        ERROR_INTERACTIVE_MODE,
        [Description("704. The callback number is invalid")]
        ERROR_BAD_CALLBACK_NUMBER,
        [Description("705. The authorization state is invalid")]
        ERROR_INVALID_AUTH_STATE,
        [Description("706. ERROR WRITING INITBPS")]
        ERROR_WRITING_INITBPS,
        [Description("707. There was an error related to the X.25 protocol")]
        ERROR_X25_DIAGNOSTIC,
        [Description("708. The account has expired")]
        ERROR_ACCT_EXPIRED,
        [Description("709. There was an error changing the password on the domain. The password might have been too short or might have matched a previously used password")]
        ERROR_CHANGING_PASSWORD,
        [Description("710. Serial overrun errors were detected while communicating with the modem")]
        ERROR_OVERRUN,
        [Description("711. The Remote Access Service Manager could not start. Additional information is provided in the event log")]
        ERROR_RASMAN_CANNOT_INITIALIZE,
        [Description("712. The two-way port is initializing. Wait a few seconds and redial")]
        ERROR_BIPLEX_PORT_NOT_AVAILABLE,
        [Description("713. No active ISDN lines are available")]
        ERROR_NO_ACTIVE_ISDN_LINES,
        [Description("714. No ISDN channels are available to make the call")]
        ERROR_NO_ISDN_CHANNELS_AVAILABLE,
        [Description("715. Too many errors occurred because of poor phone line quality")]
        ERROR_TOO_MANY_LINE_ERRORS,
        [Description("716. The Remote Access Service IP configuration is unusable")]
        ERROR_IP_CONFIGURATION,
        [Description("717. No IP addresses are available in the static pool of Remote Access Service IP addresses")]
        ERROR_NO_IP_ADDRESSES,
        [Description("718. The connection timed out waiting for a valid response from the remote computer")]
        ERROR_PPP_TIMEOUT,
        [Description("719. The connection was terminated by the remote computer")]
        ERROR_PPP_REMOTE_TERMINATED,
        [Description("720. The connection attempt failed because your computer and the remote computer could not agree on PPP control protocols")]
        ERROR_PPP_NO_PROTOCOLS_CONFIGURED,
        [Description("721. The remote computer is not responding")]
        ERROR_PPP_NO_RESPONSE,
        [Description("722. Invalid data was received from the remote computer. This data was ignored")]
        ERROR_PPP_INVALID_PACKET,
        [Description("723. The phone number, including prefix and suffix, is too long")]
        ERROR_PHONE_NUMBER_TOO_LONG,
        [Description("724. The IPX protocol cannot dial out on the modem (or other connecting device) because this computer is not configured for dialing out (it is an IPX router)")]
        ERROR_IPXCP_NO_DIALOUT_CONFIGURED,
        [Description("725. The IPX protocol cannot dial in on the modem (or other connecting device) because this computer is not configured for dialing in (the IPX router is not installed)")]
        ERROR_IPXCP_NO_DIALIN_CONFIGURED,
        [Description("726. The IPX protocol cannot be used for dialing out on more than one modem (or other connecting device) at a time")]
        ERROR_IPXCP_DIALOUT_ALREADY_ACTIVE,
        [Description("727. Cannot access TCPCFG.DLL")]
        ERROR_ACCESSING_TCPCFGDLL,
        [Description("728. The system cannot find an IP adapter")]
        ERROR_NO_IP_RAS_ADAPTER,
        [Description("729. SLIP cannot be used unless the IP protocol is installed")]
        ERROR_SLIP_REQUIRES_IP,
        [Description("730. Computer registration is not complete")]
        ERROR_PROJECTION_NOT_COMPLETE,
        [Description("731. The protocol is not configured")]
        ERROR_PROTOCOL_NOT_CONFIGURED,
        [Description("732. Your computer and the remote computer could not agree on PPP control protocols")]
        ERROR_PPP_NOT_CONVERGING,
        [Description("733. Your computer and the remote computer could not agree on PPP control protocols")]
        ERROR_PPP_CP_REJECTED,
        [Description("734. The PPP link control protocol was terminated")]
        ERROR_PPP_LCP_TERMINATED,
        [Description("735. The requested address was rejected by the server")]
        ERROR_PPP_REQUIRED_ADDRESS_REJECTED,
        [Description("736. The remote computer terminated the control protocol")]
        ERROR_PPP_NCP_TERMINATED,
        [Description("737. Loopback was detected")]
        ERROR_PPP_LOOPBACK_DETECTED,
        [Description("738. The server did not assign an address")]
        ERROR_PPP_NO_ADDRESS_ASSIGNED,
        [Description("739. The authentication protocol required by the remote server cannot use the stored password. Redial, entering the password explicitly")]
        ERROR_CANNOT_USE_LOGON_CREDENTIALS,
        [Description("740. An invalid dialing rule was detected")]
        ERROR_TAPI_CONFIGURATION,
        [Description("741. The local computer does not support the required data encryption type")]
        ERROR_NO_LOCAL_ENCRYPTION,
        [Description("742. The remote computer does not support the required data encryption type")]
        ERROR_NO_REMOTE_ENCRYPTION,
        [Description("743. The remote computer requires data encryption")]
        ERROR_REMOTE_REQUIRES_ENCRYPTION,
        [Description("744. The system cannot use the IPX network number assigned by the remote computer. Additional information is provided in the event log")]
        ERROR_IPXCP_NET_NUMBER_CONFLICT,
        [Description("745. The Session Management Module (SMM) is invalid")]
        ERROR_INVALID_SMM,
        [Description("746. The SMM is uninitialized")]
        ERROR_SMM_UNINITIALIZED,
        [Description("747. No MAC for port")]
        ERROR_NO_MAC_FOR_PORT,
        [Description("748. The SMM timed out")]
        ERROR_SMM_TIMEOUT,
        [Description("749. A bad phone number was specified")]
        ERROR_BAD_PHONE_NUMBER,
        [Description("750. The wrong SMM was specified")]
        ERROR_WRONG_MODULE,
        [Description("751. The callback number contains an invalid character. Only the following 18 characters are allowed: 0 to 9, T, P, W, (, ), -, @, and space")]
        ERROR_INVALID_CALLBACK_NUMBER,
        [Description("752. A syntax error was encountered while processing a script")]
        ERROR_SCRIPT_SYNTAX,
        [Description("753. The connection could not be disconnected because it was created by the multi-protocol router")]
        ERROR_HANGUP_FAILED,
        [Description("754. The system could not find the multi-link bundle")]
        ERROR_BUNDLE_NOT_FOUND,
        [Description("755. The system cannot perform automated dial because this connection has a custom dialer specified")]
        ERROR_CANNOT_DO_CUSTOMDIAL,
        [Description("756. This connection is already being dialed")]
        ERROR_DIAL_ALREADY_IN_PROGRESS,
        [Description("757. RAS could not be started automatically. Additional information is provided in the event log")]
        ERROR_RASAUTO_CANNOT_INITIALIZE,
        [Description("758. Internet Connection Sharing (ICS) is already enabled on the connection")]
        ERROR_CONNECTION_ALREADY_SHARED,
        [Description("759. An error occurred while the existing Internet Connection Sharing settings were being changed")]
        ERROR_SHARING_CHANGE_FAILED,
        [Description("760. An error occurred while routing capabilities were being enabled")]
        ERROR_SHARING_ROUTER_INSTALL,
        [Description("761. An error occurred while Internet Connection Sharing was being enabled for the connection")]
        ERROR_SHARE_CONNECTION_FAILED,
        [Description("762. An error occurred while the local network was being configured for sharing")]
        ERROR_SHARING_PRIVATE_INSTALL64,
        [Description("763. Internet Connection Sharing cannot be enabled. There is more than one LAN connection other than the connection to be shared")]
        ERROR_CANNOT_SHARE_CONNECTION,
        [Description("764. No smart card reader is installed")]
        ERROR_NO_SMART_CARD_READER,
        [Description("765. Internet Connection Sharing cannot be enabled. A LAN connection is already configured with the IP address that is required for automatic IP addressing. ")]
        ERROR_SHARING_ADDRESS_EXISTS,
        [Description("766. A certificate could not be found. Connections that use the L2TP protocol over IPSec require the installation of a machine certificate, also known as a computer certificate. ")]
        ERROR_NO_CERTIFICATE,
        [Description("767. Internet Connection Sharing cannot be enabled. The LAN connection selected as the private network has more than one IP address configured. Please reconfigure the LAN connection with a single IP address before enabling Internet Connection Sharing. ")]
        ERROR_SHARING_MULTIPLE_ADDRESSES,
        [Description("768. The connection attempt failed because of failure to encrypt data. ")]
        ERROR_FAILED_TO_ENCRYPT,
        [Description("769. The specified destination is not reachable. ")]
        ERROR_BAD_ADDRESS_SPECIFIED,
        [Description("770. The remote computer rejected the connection attempt. ")]
        ERROR_CONNECTION_REJECT,
        [Description("771. The connection attempt failed because the network is busy. ")]
        ERROR_CONGESTION,
        [Description("772. The remote computer's network hardware is incompatible with the type of call requested. ")]
        ERROR_INCOMPATIBLE,
        [Description("773. The connection attempt failed because the destination number has changed. ")]
        ERROR_NUMBERCHANGED,
        [Description("774. The connection attempt failed because of a temporary failure. Try connecting again")]
        ERROR_TEMPFAILURE,
        [Description("775. The call was blocked by the remote computer. ")]
        ERROR_BLOCKED,
        [Description("776. The call could not be connected because the remote computer has invoked the Do Not Disturb feature. ")]
        ERROR_DONOTDISTURB,
        [Description("777. The connection attempt failed because the modem or other connection device on the remote computer is out of order. ")]
        ERROR_OUTOFORDER,
        [Description("778. It was not possible to verify the identity of the server. ")]
        ERROR_UNABLE_TO_AUTHENTICATE_SERVER,
        [Description("779. To dial out using this connection you must use a smart card")]
        ERROR_SMART_CARD_REQUIRED,
        [Description("780. An attempted function is not valid for this connection. ")]
        ERROR_INVALID_FUNCTION_FOR_ENTRY,
        [Description("781. The encryption attempt failed because no valid certificate was found")]
        ERROR_CERT_FOR_ENCRYPTION_NOT_FOUND,
        [Description("782. Connection Sharing (NAT) is currently installed as a routing protocol, and must be removed before enabling Internet Connection Sharing")]
        ERROR_SHARING_RRAS_CONFLICT,
        [Description("783. Internet Connection Sharing cannot be enabled. The LAN connection selected as the private network is either not present, or is disconnected from the network. Please ensure that the LAN adapter is connected before enabling Internet Connection Sharing. ")]
        ERROR_SHARING_NO_PRIVATE_LAN,
        [Description("784. You cannot dial using this connection at login time because it is configured to use a user name different than the one on the smart card. If you want to use this connection at login time, you must configure it to use the user name on the smart card. ")]
        ERROR_NO_DIFF_USER_AT_LOGON,
        [Description("785. You cannot dial using this connection at login time because it is not configured to use a smart card. If you want to use it at login time, you must edit the properties of this connection so that it uses a smart card. ")]
        ERROR_NO_REG_CERT_AT_LOGON,
        [Description("786. The L2TP connection attempt failed because there is no valid machine certificate on your computer for security authentication. ")]
        ERROR_OAKLEY_NO_CERT,
        [Description("787. The L2TP connection attempt failed because the security layer could not authenticate the remote computer. ")]
        ERROR_OAKLEY_AUTH_FAIL,
        [Description("788. The L2TP connection attempt failed because the security layer could not negotiate compatible parameters with the remote computer. ")]
        ERROR_OAKLEY_ATTRIB_FAIL,
        [Description("789. The L2TP connection attempt failed because the security layer encountered a processing error during initial negotiations with the remote computer. ")]
        ERROR_OAKLEY_GENERAL_PROCESSING,
        [Description("790. The L2TP connection attempt failed because certificate validation on the remote computer failed. ")]
        ERROR_OAKLEY_NO_PEER_CERT,
        [Description("791. The L2TP connection attempt failed because security policy for the connection was not found. ")]
        ERROR_OAKLEY_NO_POLICY,
        [Description("792. The L2TP connection attempt failed because security negotiation timed out. ")]
        ERROR_OAKLEY_TIMED_OUT,
        [Description("793. The L2TP connection attempt failed because an error occurred while negotiating security. ")]
        ERROR_OAKLEY_ERROR,
        [Description("794. The Framed Protocol RADIUS attribute for this user is not PPP. ")]
        ERROR_UNKNOWN_FRAMED_PROTOCOL,
        [Description("795. The Tunnel Type RADIUS attribute for this user is not correct. ")]
        ERROR_WRONG_TUNNEL_TYPE,
        [Description("796. The Service Type RADIUS attribute for this user is neither Framed nor Callback Framed. ")]
        ERROR_UNKNOWN_SERVICE_TYPE,
        [Description("797. A connection to the remote computer could not be established because the modem was not found or was busy. ")]
        ERROR_CONNECTING_DEVICE_NOT_FOUND,
        [Description("798. A certificate could not be found that can be used with the Extensible Authentication Protocol (EAP). ")]
        ERROR_NO_EAPTLS_CERTIFICATE,
        [Description("799. Internet Connection Sharing (ICS) cannot be enabled due to an IP address conflict on the network. ICS requires the host be configured to use 192.168.0.1. Ensure that no other client on the network is configured to use 192.168.0.1. ")]
        ERROR_SHARING_HOST_ADDRESS_CONFLICT,
        [Description("800. Unable to establish the VPN connection. The VPN server may be unreachable, or security parameters may not be configured properly for this connection. ")]
        ERROR_AUTOMATIC_VPN_FAILED,
        [Description("801. This connection is configured to validate the identity of the access server, but Windows cannot verify the digital certificate sent by the server")]
        ERROR_VALIDATING_SERVER_CERT,
        [Description("802. The card supplied was not recognized. Please check that the card is inserted correctly, and fits securely")]
        ERROR_READING_SCARD,
        [Description("803. The PEAP configuration stored in the session cookie does not match the current session configuration")]
        ERROR_INVALID_PEAP_COOKIE_CONFIG,
        [Description("804. The PEAP identity stored in the session cookie does not match the current identity")]
        ERROR_INVALID_PEAP_COOKIE_USER,
        [Description("805. You cannot dial using this connection at login time because it is configured to use the currently-logged-in user's credentials")]
        ERROR_INVALID_MSCHAPV2_CONFIG,
        [Description("806. A connection between your computer and the VPN server has been started, but the VPN connection cannot be completed. The most common cause for this is that at least one Internet device (for example, a firewall or a router) between your computer and the VPN server is not configured to allow Generic Routing Encapsulation (GRE) protocol packets")]
        ERROR_VPN_GRE_BLOCKED,
        [Description("807. The network connection between your computer and the VPN server was interrupted. This can be caused by a problem in the VPN transmission and is commonly the result of internet latency or simply that your VPN server has reached capacity. Try to reconnect to the VPN server")]
        ERROR_VPN_DISCONNECT,
        [Description("808. The network connection between your computer and the VPN server could not be established because the remote server refused the connection. This is typically caused by a mismatch between the server's configuration and your connection settings")]
        ERROR_VPN_REFUSED,
        [Description("809. The network connection between your computer and the VPN server could not be established because the remote server is not responding. This could be because one of the network devices (for example, firewalls, NAT, routers) between your computer and the remote server is not configured to allow VPN connections")]
        ERROR_VPN_TIMEOUT,
        [Description("810. A network connection between your computer and the VPN server was started, but the VPN connection was not completed. This is typically caused by the use of an incorrect or expired certificate for authentication between the client and the server")]
        ERROR_VPN_BAD_CERT,
        [Description("811. The network connection between your computer and the VPN server could not be established because the remote server is not responding. This is typically caused by a pre-shared key problem between the client and server. A pre-shared key is used to guarantee you are who you say you are in an IP Security (IPSec) communication cycle")]
        ERROR_VPN_BAD_PSK,
        [Description("812. The connection was prevented because of a policy configured on your RAS/VPN server. Specifically, the authentication method used by the server to verify your username and password may not match the authentication method configured in your connection profile")]
        ERROR_SERVER_POLICY,
        [Description("813. You have attempted to establish a second broadband connection while a previous broadband connection is already established using the same device or port")]
        ERROR_BROADBAND_ACTIVE,
        [Description("814. The underlying Ethernet connectivity required for the broadband connection was not found")]
        ERROR_BROADBAND_NO_NIC,
        [Description("815. The broadband network connection could not be established on your computer because the remote server is not responding. This could be caused by an invalid value for the 'Service Name' field for this connection")]
        ERROR_BROADBAND_TIMEOUT,
        [Description("816. A feature or setting you have tried to enable is no longer supported by the remote access service")]
        ERROR_FEATURE_DEPRECATED,
        [Description("817. Cannot delete a connection while it is connected")]
        ERROR_CANNOT_DELETE,
        [Description("818. The Network Access Protection (NAP) enforcement client could not create system resources for remote access connections. Some network services or resources might not be available")]
        ERROR_RASQEC_RESOURCE_CREATION_FAILED,
        [Description("819. The Network Access Protection Agent (NAP Agent) service has been disabled or is not installed on this computer. Some network services or resources might not be available")]
        ERROR_RASQEC_NAPAGENT_NOT_ENABLED,
        [Description("820. The Network Access Protection (NAP) enforcement client failed to register with the Network Access Protection Agent (NAP Agent) service. Some network services or resources might not be available")]
        ERROR_RASQEC_NAPAGENT_NOT_CONNECTED,
        [Description("821. The Network Access Protection (NAP) enforcement client was unable to process the request because the remote access connection does not exist")]
        ERROR_RASQEC_CONN_DOESNOTEXIST,
        [Description("822. The Network Access Protection (NAP) enforcement client did not respond. Some network services or resources might not be available")]
        ERROR_RASQEC_TIMEOUT,
        [Description("823. Received Crypto-Binding type-length-value (TLV) is invalid")]
        ERROR_PEAP_CRYPTOBINDING_INVALID,
        [Description("824. Crypto-Binding TLV was not received")]
        ERROR_PEAP_CRYPTOBINDING_NOTRECEIVED,
        [Description("825. Point-to-Point Tunneling Protocol (PPTP) is incompatible with IPv6. Change the type of virtual private network to Layer Two Tunneling Protocol (L2TP)")]
        ERROR_INVALID_VPNSTRATEGY,
        [Description("826. EAPTLS validation of the cached credentials failed. Discard cached credentials")]
        ERROR_EAPTLS_CACHE_CREDENTIALS_INVALID,
        [Description("827. The L2TP/IPsec connection cannot be completed because the IKE and AuthIP IPSec Keying Modules service and/or the Base Filtering Engine service is not running. These services are required to establish an L2TP/IPSec connection")]
        ERROR_IPSEC_SERVICE_STOPPED,
        [Description("828. The connection was terminated because of idle timeout")]
        ERROR_IDLE_TIMEOUT,
        [Description("829. The modem (or other connecting device) was disconnected due to link failure")]
        ERROR_LINK_FAILURE,
        [Description("830. The connection was terminated because user logged off")]
        ERROR_USER_LOGOFF,
        [Description("831. The connection was terminated because user switch happened")]
        ERROR_FAST_USER_SWITCH,
        [Description("832. The connection was terminated because of hibernation")]
        ERROR_HIBERNATION,
        [Description("833. The connection was terminated because the system got suspended")]
        ERROR_SYSTEM_SUSPENDED,
        [Description("834. The connection was terminated because Remote Access Connection manager stopped")]
        ERROR_RASMAN_SERVICE_STOPPED,
        [Description("835. The L2TP connection attempt failed because the security layer could not authenticate the remote computer. This could be because one or more fields of the certificate presented by the remote server could not be validated as belonging to the target destination")]
        ERROR_INVALID_SERVER_CERT,
        [Description("836. The machine is not NAP capable")]
        ERROR_NOT_NAP_CAPABLE
    }
}