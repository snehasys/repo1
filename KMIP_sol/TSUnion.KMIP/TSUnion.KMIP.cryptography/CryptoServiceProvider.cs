using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Cryptography.Providers;


namespace TSUnion.KMIP.Cryptography
{
    public class CryptoServiceProvider
    {
        public static ICryptoProvider GetCryptoProvider(KeyCryptographicAlgorithmType type) 
        {
            switch (type) 
            {
                case KeyCryptographicAlgorithmType.AES: { return new AESProvider(); }
                case KeyCryptographicAlgorithmType.DES3: { return new DES3Provider(); }
                case KeyCryptographicAlgorithmType.Blowfish: { return new BlowfishProvider(); }
             
                default: {return null;}
            }
           
        }
    }
}
