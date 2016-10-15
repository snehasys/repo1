using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    [Serializable]
    public enum ResultStatusType
    {
        Success = 00000000,
        OperationFailed = 00000001,
        OperationPending = 00000002,
        OperationUndone = 00000003,
        Extensions = 80000003

    }
}
