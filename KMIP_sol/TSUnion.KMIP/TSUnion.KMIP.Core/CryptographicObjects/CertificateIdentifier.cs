using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    public class CertificateIdentifier
    {
        public string IssuerDistinguishedName { get; set; }
        public string CertificateSerialNumber { get; set; }

    }
}
