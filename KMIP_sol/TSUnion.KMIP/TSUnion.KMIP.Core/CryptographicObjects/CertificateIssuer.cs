using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
  public  class CertificateIssuer
    {
        public string IssuerDistinguishedName { get; set; }
        public string IssuerAlternativeName { get; set; }
    }
}
