using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class RevocationReason
    {
       public RevocationReasonType Type { get; set; }
       public string Message { get; set; }
    }
}
