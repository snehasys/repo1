using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class CryptographicUsageMask
    {
        public bool Sign { get; set; }
        public bool Verify { get; set; }
        public bool Encrypt { get; set; }
        public bool Decrypt { get; set; }
        public bool WrapKey { get; set; }
        public bool UnwrapKey { get; set; }
        public bool Export { get; set; }
        public bool MACGenerate { get; set; }
        public bool MACVerify { get; set; }
        public bool DeriveKey { get; set; }
        public bool ContentCommitment { get; set; }
        public bool KeyAgreement { get; set; }
        public bool CertificateSign { get; set; }
        public bool CRLSign { get; set; }
        public bool GenerateCryptogram { get; set; }
        public bool ValidateCryptogram { get; set; }
        public bool TranslateEncrypt { get; set; }
        public bool TranslateDecrypt { get; set; }
        public bool TranslateWrap { get; set; }
        public bool TranslateUnwrap { get; set; }
    }
}
