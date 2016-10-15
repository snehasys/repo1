using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class KeyBlockBase
    {

        public int Length { get; set; }
        public KeyCryptographicAlgorithmType Algorithm { get; set; }
        public KeyCompressionType Compression { get; set; }
        public KeyFormatType Format { get; set; }

        public byte[] Raw { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
        public byte[] Opaque { get { throw new NotImplementedException(); } set { } }
        public object PKCS1 { get { throw new NotImplementedException(); } set { } }
        public object PKCS8 { get { throw new NotImplementedException(); } set { } }
        public object X509 { get { throw new NotImplementedException(); } set { } }
        public object ECPrivateKey { get { throw new NotImplementedException(); } set { } }
        public object Extensions { get { throw new NotImplementedException(); } set { } }

        public object KeyWrappingData { get { throw new NotImplementedException(); } set { } }

    }
}
