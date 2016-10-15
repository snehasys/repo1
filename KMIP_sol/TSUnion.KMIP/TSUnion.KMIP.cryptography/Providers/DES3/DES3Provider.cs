using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TSUnion.KMIP.Cryptography;

namespace TSUnion.KMIP.Cryptography.Providers
{
    public class DES3Provider : ICryptoProvider
    {
        TripleDESCryptoServiceProvider keyWrapper;
        

          //                      keyBlock.Value = keyWrapper.Key;
          //                      cryptoObject.Key = keyBlock;
        #region Члены ICryptoProvider



        public DES3Provider()
        {
            keyWrapper = new TripleDESCryptoServiceProvider();
            keyWrapper.GenerateIV();
            keyWrapper.GenerateKey();

        }

        public byte[] GetSalt()
        {
            return keyWrapper.IV;
        }

        public int GetKeySize()
        {
            return keyWrapper.KeySize;
        }

        public byte[] GetKey()
        {
          return  keyWrapper.Key;
        }

        public int GetDefaultIteration()
        {
            return 16;
        }

        #endregion
    }
}
