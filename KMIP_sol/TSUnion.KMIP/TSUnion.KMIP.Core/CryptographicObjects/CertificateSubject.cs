using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    public class CertificateSubject
    {
        public string SubjectDistinguishedName  { get; set; }
        public string SubjectAlternativeName { get; set; }
    }
}
