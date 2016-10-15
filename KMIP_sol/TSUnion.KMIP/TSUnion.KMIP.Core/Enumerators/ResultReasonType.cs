using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    [Serializable]
    public enum ResultReasonType
    {
        ItemNotFound = 0x00000001,
        ResponseTooLarge = 0x00000002,
        AuthenticationNotSuccessful = 0x00000003,
        InvalidMessage = 0x00000004,
        OperationNotSupported = 0x00000005,
        MissingData = 0x00000006,
        InvalidField = 0x00000007,
        FeatureNotSupported = 0x00000008,
        OperationCanceledByRequester = 0x00000009,
        CryptographicFailure = 0x0000000A,
        IllegalOperation = 0x0000000B,
        PermissionDenied = 0x0000000C,
        ObjectArchived = 0x0000000D,
        IndexOutOfBounds = 0x0000000E,
        ApplicationNamespaceNotSupported = 0x0000000F,
        KeyFormatTypeNotSupported = 0x00000010,
        KeyCompressionTypeNotSupported = 0x00000011,
        GeneralFailure = 0x00000100,
        Extensions = 81111111
    }
}
