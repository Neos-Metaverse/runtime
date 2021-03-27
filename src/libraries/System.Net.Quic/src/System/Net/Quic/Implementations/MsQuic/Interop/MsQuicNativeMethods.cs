// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;

namespace System.Net.Quic.Implementations.MsQuic.Internal
{
    /// <summary>
    /// Contains all native delegates and structs that are used with MsQuic.
    /// </summary>
    internal static unsafe class MsQuicNativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct NativeApi
        {
            internal IntPtr SetContext;
            internal IntPtr GetContext;
            internal IntPtr SetCallbackHandler;

            internal IntPtr SetParam;
            internal IntPtr GetParam;

            internal IntPtr RegistrationOpen;
            internal IntPtr RegistrationClose;
            internal IntPtr RegistrationShutdown;

            internal IntPtr ConfigurationOpen;
            internal IntPtr ConfigurationClose;
            internal IntPtr ConfigurationLoadCredential;

            internal IntPtr ListenerOpen;
            internal IntPtr ListenerClose;
            internal IntPtr ListenerStart;
            internal IntPtr ListenerStop;

            internal IntPtr ConnectionOpen;
            internal IntPtr ConnectionClose;
            internal IntPtr ConnectionShutdown;
            internal IntPtr ConnectionStart;
            internal IntPtr ConnectionSetConfiguration;
            internal IntPtr ConnectionSendResumptionTicket;

            internal IntPtr StreamOpen;
            internal IntPtr StreamClose;
            internal IntPtr StreamStart;
            internal IntPtr StreamShutdown;
            internal IntPtr StreamSend;
            internal IntPtr StreamReceiveComplete;
            internal IntPtr StreamReceiveSetEnabled;

            internal IntPtr DatagramSend;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint MsQuicOpenDelegate(
            out NativeApi* registration);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint SetContextDelegate(
            SafeHandle handle,
            IntPtr context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetContextDelegate(
            SafeHandle handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetCallbackHandlerDelegate(
            SafeHandle handle,
            Delegate del,
            IntPtr context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint SetParamDelegate(
            SafeHandle handle,
            QUIC_PARAM_LEVEL level,
            uint param,
            uint bufferLength,
            byte* buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint GetParamDelegate(
            SafeHandle handle,
            QUIC_PARAM_LEVEL level,
            uint param,
            ref uint bufferLength,
            byte* buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint RegistrationOpenDelegate(
            ref RegistrationConfig config,
            out SafeMsQuicRegistrationHandle registrationContext);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RegistrationCloseDelegate(
            IntPtr registrationContext);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RegistrationConfig
        {
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string AppName;
            internal QUIC_EXECUTION_PROFILE ExecutionProfile;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConfigurationOpenDelegate(
            SafeMsQuicRegistrationHandle registrationContext,
            QuicBuffer* alpnBuffers,
            uint alpnBufferCount,
            ref QuicSettings settings,
            uint settingsSize,
            IntPtr context,
            out SafeMsQuicConfigurationHandle configuration);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ConfigurationCloseDelegate(
            IntPtr configuration);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConfigurationLoadCredentialDelegate(
            SafeMsQuicConfigurationHandle configuration,
            ref CredentialConfig credConfig);

        [StructLayout(LayoutKind.Sequential)]
        internal struct QuicSettings
        {
            internal QuicSettingsIsSetFlags IsSetFlags;
            internal ulong MaxBytesPerKey;
            internal ulong HandshakeIdleTimeoutMs;
            internal ulong IdleTimeoutMs;
            internal uint TlsClientMaxSendBuffer;
            internal uint TlsServerMaxSendBuffer;
            internal uint StreamRecvWindowDefault;
            internal uint StreamRecvBufferDefault;
            internal uint ConnFlowControlWindow;
            internal uint MaxWorkerQueueDelayUs;
            internal uint MaxStatelessOperations;
            internal uint InitialWindowPackets;
            internal uint SendIdleTimeoutMs;
            internal uint InitialRttMs;
            internal uint MaxAckDelayMs;
            internal uint DisconnectTimeoutMs;
            internal uint KeepAliveIntervalMs;
            internal ushort PeerBidiStreamCount;
            internal ushort PeerUnidiStreamCount;
            internal ushort RetryMemoryLimit;              // Global only
            internal ushort LoadBalancingMode;             // Global only
            internal byte MaxOperationsPerDrain;
            internal QuicSettingsEnabledFlagsFlags EnabledFlags;
            internal uint* DesiredVersionsList;
            internal uint DesiredVersionsListLength;
        }

        [Flags]
        internal enum QuicSettingsIsSetFlags : ulong
        {
            MaxBytesPerKey = 1 << 0,
            HandshakeIdleTimeoutMs = 1 << 1,
            IdleTimeoutMs = 1 << 2,
            TlsClientMaxSendBuffer = 1 << 3,
            TlsServerMaxSendBuffer = 1 << 4,
            StreamRecvWindowDefault = 1 << 5,
            StreamRecvBufferDefault = 1 << 6,
            ConnFlowControlWindow = 1 << 7,
            MaxWorkerQueueDelayUs = 1 << 8,
            MaxStatelessOperations = 1 << 9,
            InitialWindowPackets = 1 << 10,
            SendIdleTimeoutMs = 1 << 11,
            InitialRttMs = 1 << 12,
            MaxAckDelayMs = 1 << 13,
            DisconnectTimeoutMs = 1 << 14,
            KeepAliveIntervalMs = 1 << 15,
            PeerBidiStreamCount = 1 << 16,
            PeerUnidiStreamCount = 1 << 17,
            RetryMemoryLimit = 1 << 18,
            LoadBalancingMode = 1 << 19,
            MaxOperationsPerDrain = 1 << 20,
            SendBufferingEnabled = 1 << 21,
            PacingEnabled = 1 << 22,
            MigrationEnabled = 1 << 23,
            DatagramReceiveEnabled = 1 << 24,
            ServerResumptionLevel = 1 << 25,
            DesiredVersionsList = 1 << 26,
            VersionNegotiationExtEnabled = 1 << 27,
        }

        [Flags]
        internal enum QuicSettingsEnabledFlagsFlags : byte
        {
            SendBufferingEnabled = 1 << 0,
            PacingEnabled = 1 << 1,
            MigrationEnabled = 1 << 2,
            DatagramReceiveEnabled = 1 << 3,
            // Contains values of QUIC_SERVER_RESUMPTION_LEVEL
            ServerResumptionLevel = 1 << 4 | 1 << 5,
            VersionNegotiationExtEnabled = 1 << 6,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CredentialConfig
        {
            internal QUIC_CREDENTIAL_TYPE Type;
            internal QUIC_CREDENTIAL_FLAGS Flags;
            // CredentialConfigCertificateUnion*
            internal IntPtr Certificate;
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string Principal;
            internal IntPtr Reserved; // Currently unused
            // TODO: define delegate for AsyncHandler and make proper use of it.
            internal IntPtr AsyncHandler;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct CredentialConfigCertificateUnion
        {
            [FieldOffset(0)]
            internal CredentialConfigCertificateCertificateHash CertificateHash;

            [FieldOffset(0)]
            internal CredentialConfigCertificateCertificateHashStore CertificateHashStore;

            [FieldOffset(0)]
            internal IntPtr CertificateContext;

            [FieldOffset(0)]
            internal CredentialConfigCertificateCertificateFile CertificateFile;

            [FieldOffset(0)]
            internal CredentialConfigCertificateCertificateFileProtected CertificateFileProtected;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CredentialConfigCertificateCertificateHash
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            internal byte[] ShaHash;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CredentialConfigCertificateCertificateHashStore
        {
            internal QUIC_CERTIFICATE_HASH_STORE_FLAGS Flags;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            internal byte[] ShaHash;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal char[] StoreName;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CredentialConfigCertificateCertificateFile
        {
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string PrivateKeyFile;

            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string CertificateFile;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CredentialConfigCertificateCertificateFileProtected
        {
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string PrivateKeyFile;

            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string CertificateFile;

            [MarshalAs(UnmanagedType.LPUTF8Str)]
            internal string PrivateKeyPassword;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ListenerEvent
        {
            internal QUIC_LISTENER_EVENT Type;
            internal ListenerEventDataUnion Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct ListenerEventDataUnion
        {
            [FieldOffset(0)]
            internal ListenerEventDataNewConnection NewConnection;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ListenerEventDataNewConnection
        {
            internal NewConnectionInfo* Info;
            internal IntPtr Connection;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NewConnectionInfo
        {
            internal uint QuicVersion;
            // QUIC_ADDR
            internal IntPtr LocalAddress;
            // QUIC_ADDR
            internal IntPtr RemoteAddress;
            internal uint CryptoBufferLength;
            internal ushort ClientAlpnListLength;
            internal ushort ServerNameLength;
            internal byte NegotiatedAlpnLength;
            // byte[]
            internal IntPtr CryptoBuffer;
            // byte[]
            internal IntPtr ClientAlpnList;
            // byte[]
            internal IntPtr NegotiatedAlpn;
            // string
            internal IntPtr ServerName;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ListenerCallbackDelegate(
            IntPtr listener,
            IntPtr context,
            ref ListenerEvent evt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ListenerOpenDelegate(
           SafeMsQuicRegistrationHandle registration,
           ListenerCallbackDelegate handler,
           IntPtr context,
           out SafeMsQuicListenerHandle listener);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ListenerCloseDelegate(
            IntPtr listener);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ListenerStartDelegate(
            SafeMsQuicListenerHandle listener,
            QuicBuffer* alpnBuffers,
            uint alpnBufferCount,
            ref SOCKADDR_INET localAddress);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ListenerStopDelegate(
            SafeMsQuicListenerHandle listener);

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataConnected
        {
            internal byte SessionResumed;
            internal byte NegotiatedAlpnLength;
            // byte[]
            internal IntPtr NegotiatedAlpn;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataShutdownInitiatedByTransport
        {
            internal uint Status;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataShutdownInitiatedByPeer
        {
            internal long ErrorCode;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataShutdownComplete
        {
            // The flags have fixed sized exactly 3 bits
            internal ConnectionEventDataShutdownCompleteFlags Flags;
        }

        [Flags]
        internal enum ConnectionEventDataShutdownCompleteFlags : byte
        {
            HandshakeCompleted = 1 << 0,
            PeerAcknowledgedShutdown = 1 << 1,
            AppCloseInProgress = 1 << 2,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataLocalAddressChanged
        {
            // QUIC_ADDR
            internal IntPtr Address;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataPeerAddressChanged
        {
            // QUIC_ADDR
            internal IntPtr Address;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataPeerStreamStarted
        {
            internal IntPtr Stream;
            internal QUIC_STREAM_OPEN_FLAGS Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEventDataStreamsAvailable
        {
            internal ushort BiDirectionalCount;
            internal ushort UniDirectionalCount;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct ConnectionEventDataUnion
        {
            [FieldOffset(0)]
            internal ConnectionEventDataConnected Connected;

            [FieldOffset(0)]
            internal ConnectionEventDataShutdownInitiatedByTransport ShutdownInitiatedByTransport;

            [FieldOffset(0)]
            internal ConnectionEventDataShutdownInitiatedByPeer ShutdownInitiatedByPeer;

            [FieldOffset(0)]
            internal ConnectionEventDataShutdownComplete ShutdownComplete;

            [FieldOffset(0)]
            internal ConnectionEventDataLocalAddressChanged LocalAddressChanged;

            [FieldOffset(0)]
            internal ConnectionEventDataPeerAddressChanged PeerAddressChanged;

            [FieldOffset(0)]
            internal ConnectionEventDataPeerStreamStarted PeerStreamStarted;

            [FieldOffset(0)]
            internal ConnectionEventDataStreamsAvailable StreamsAvailable;

            // TODO: missing IDEAL_PROCESSOR_CHANGED, ..., PEER_CERTIFICATE_RECEIVED (7 total)
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ConnectionEvent
        {
            internal QUIC_CONNECTION_EVENT_TYPE Type;
            internal ConnectionEventDataUnion Data;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConnectionCallbackDelegate(
            IntPtr connection,
            IntPtr context,
            ref ConnectionEvent connectionEvent);

        // TODO: order is Open, Close, Shutdown, Start, SetConfiguration, SendResumptionTicket
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConnectionOpenDelegate(
            SafeMsQuicRegistrationHandle registration,
            ConnectionCallbackDelegate handler,
            IntPtr context,
            out SafeMsQuicConnectionHandle connection);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ConnectionCloseDelegate(
            IntPtr connection);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConnectionSetConfigurationDelegate(
            SafeMsQuicConnectionHandle connection,
            SafeMsQuicConfigurationHandle configuration);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint ConnectionStartDelegate(
            SafeMsQuicConnectionHandle connection,
            SafeMsQuicConfigurationHandle configuration,
            QUIC_ADDRESS_FAMILY family,
            [MarshalAs(UnmanagedType.LPUTF8Str)]
            string serverName,
            ushort serverPort);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ConnectionShutdownDelegate(
            SafeMsQuicConnectionHandle connection,
            QUIC_CONNECTION_SHUTDOWN_FLAGS flags,
            long errorCode);

        // TODO: missing SendResumptionTicket

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEventDataReceive
        {
            internal ulong AbsoluteOffset;
            internal ulong TotalBufferLength;
            internal QuicBuffer* Buffers;
            internal uint BufferCount;
            internal QUIC_RECEIVE_FLAGS Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEventDataSendComplete
        {
            internal byte Canceled;
            internal IntPtr ClientContext;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEventDataPeerSendAborted
        {
            internal long ErrorCode;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEventDataPeerReceiveAborted
        {
            internal long ErrorCode;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEventDataSendShutdownComplete
        {
            internal byte Graceful;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct StreamEventDataUnion
        {
            // TODO: missing START_COMPLETE
            [FieldOffset(0)]
            internal StreamEventDataReceive Receive;

            [FieldOffset(0)]
            internal StreamEventDataSendComplete SendComplete;

            [FieldOffset(0)]
            internal StreamEventDataPeerSendAborted PeerSendAborted;

            [FieldOffset(0)]
            internal StreamEventDataPeerReceiveAborted PeerReceiveAborted;

            [FieldOffset(0)]
            internal StreamEventDataSendShutdownComplete SendShutdownComplete;

            // TODO: missing IDEAL_SEND_BUFFER_SIZE
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StreamEvent
        {
            internal QUIC_STREAM_EVENT_TYPE Type;
            internal StreamEventDataUnion Data;
        }

        // TODO: rename to C#-like
        [StructLayout(LayoutKind.Sequential)]
        internal struct SOCKADDR_IN
        {
            internal ushort sin_family;
            internal ushort sin_port;
            internal byte sin_addr0;
            internal byte sin_addr1;
            internal byte sin_addr2;
            internal byte sin_addr3;

            internal byte[] Address
            {
                get
                {
                    return new byte[] { sin_addr0, sin_addr1, sin_addr2, sin_addr3 };
                }
            }
        }

        // TODO: rename to C#-like
        [StructLayout(LayoutKind.Sequential)]
        internal struct SOCKADDR_IN6
        {
            internal ushort _family;
            internal ushort _port;
            internal uint _flowinfo;
            internal byte _addr0;
            internal byte _addr1;
            internal byte _addr2;
            internal byte _addr3;
            internal byte _addr4;
            internal byte _addr5;
            internal byte _addr6;
            internal byte _addr7;
            internal byte _addr8;
            internal byte _addr9;
            internal byte _addr10;
            internal byte _addr11;
            internal byte _addr12;
            internal byte _addr13;
            internal byte _addr14;
            internal byte _addr15;
            internal uint _scope_id;

            internal byte[] Address
            {
                get
                {
                    return new byte[] {
                    _addr0, _addr1, _addr2, _addr3,
                    _addr4, _addr5, _addr6, _addr7,
                    _addr8, _addr9, _addr10, _addr11,
                    _addr12, _addr13, _addr14, _addr15 };
                }
            }
        }

        // TODO: rename to C#-like
        [StructLayout(LayoutKind.Explicit)]
        internal struct SOCKADDR_INET
        {
            [FieldOffset(0)]
            internal SOCKADDR_IN Ipv4;
            [FieldOffset(0)]
            internal SOCKADDR_IN6 Ipv6;
            [FieldOffset(0)]
            internal ushort si_family;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamCallbackDelegate(
            IntPtr stream,
            IntPtr context,
            ref StreamEvent streamEvent);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamOpenDelegate(
            SafeMsQuicConnectionHandle connection,
            QUIC_STREAM_OPEN_FLAGS flags,
            StreamCallbackDelegate handler,
            IntPtr context,
            out SafeMsQuicStreamHandle stream);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamStartDelegate(
            SafeMsQuicStreamHandle stream,
            QUIC_STREAM_START_FLAGS flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamCloseDelegate(
            IntPtr stream);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamShutdownDelegate(
            SafeMsQuicStreamHandle stream,
            QUIC_STREAM_SHUTDOWN_FLAGS flags,
            long errorCode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamSendDelegate(
            SafeMsQuicStreamHandle stream,
            QuicBuffer* buffers,
            uint bufferCount,
            QUIC_SEND_FLAGS flags,
            IntPtr clientSendContext);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamReceiveCompleteDelegate(
            SafeMsQuicStreamHandle stream,
            ulong bufferLength);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint StreamReceiveSetEnabledDelegate(
            SafeMsQuicStreamHandle stream,
            [MarshalAs(UnmanagedType.U1)]
            bool enabled);

        [StructLayout(LayoutKind.Sequential)]
        internal struct QuicBuffer
        {
            internal uint Length;
            internal byte* Buffer;
        }

        // TODO: DatagramSend missing
    }
}