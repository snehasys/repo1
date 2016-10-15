using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum RevocationReasonType
    {
        Unspecified = 0x00000001,
        KeyCompromise = 0x00000002,
        CACompromise = 0x00000003,
        AffiliationChanged = 0x00000004,
        Superseded = 0x00000005,
        CessationOperation = 0x00000006,
        PrivilegeWithdrawn = 0x00000007

    }
}
