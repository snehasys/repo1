using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class DerivationParameters
    {
        public CryptographicParameters CryptoParameters { get; set; }
        public byte[] InitializationVector { get; set; }
        public byte[] DerivationData { get; set; }
        public byte[] Salt;
    
    
        public int IterationCount { get; set; }

        public static DerivationParameters Create() 
        {
            return new DerivationParameters();
        }

    }
}
