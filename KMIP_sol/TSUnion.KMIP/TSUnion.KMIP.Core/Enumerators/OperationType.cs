using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum OperationType
    {
        Create = 00000001,
        CreateKeyPair = 00000002,
        Register = 00000003,
        ReKey = 00000004,
        DeriveKey = 00000005,
        Certify = 00000006,
        ReCertify = 00000007,
        Locate = 00000008,
        Check = 00000009,
        Get = 0x0000000A,
        GetAttributes = 0x0000000B,
        GetAttributeList = 0x0000000C,
        AddAttribute = 0x0000000D,
        ModifyAttribute = 0x0000000E,
        DeleteAttribute = 0x0000000F,
        ObtainLease = 0x00000010,
        GetUsageAllocation = 0x00000011,
        Activate = 0x00000012,
        Revoke = 0x00000013,
        Destroy = 0x00000014,
        Archive = 0x00000015,
        Recover = 0x00000016,
        Validate = 0x00000017,
        Query = 0x00000018,
        Cancel = 0x00000019,
        Poll = 0x0000001A,
        Notify = 0x0000001B,
        Put = 0x0000001C,
        Extensions = 0x8000001,
        ReKeyPair =0x0000001D,
        Delete = 0x0000001E,
        DeleteAll = 0x0000001F,
    }
}
