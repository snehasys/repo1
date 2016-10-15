using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TSUnion.KMIP.Cryptography;

namespace TSUnion.KMIP.Cryptography.Providers
{
    public class AESProvider : ICryptoProvider
    {
         AesCryptoServiceProvider keyWrapper = new AesCryptoServiceProvider();
                              

                                

   

        public AESProvider()
        {
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
           return keyWrapper.Key;
        }

        

        #region Члены ICryptoProvider<AESProvider>

        public AESProvider Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        public int GetDefaultIteration()
        {
            return 32;
        }
    }
}
