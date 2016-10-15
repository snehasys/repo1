using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum CertificateType
    {
        X509 = 00000001,
        PGP = 00000002,
        Extensions = 80000000
    }
}
