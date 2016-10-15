using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Cryptography.Providers
{
    class BlowfishProvider :ICryptoProvider
    {
        #region Члены ICryptoProvider

        public byte[] GetSalt()
        {
            throw new NotImplementedException();
        }

        public int GetKeySize()
        {
            throw new NotImplementedException();
        }

        public byte[] GetKey()
        {
            throw new NotImplementedException();
        }

        public int GetDefaultIteration()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
