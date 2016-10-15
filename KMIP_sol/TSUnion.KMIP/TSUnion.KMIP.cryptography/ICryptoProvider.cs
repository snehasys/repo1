using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Cryptography
{
   public interface ICryptoProvider  
    {
        
        byte[] GetSalt();
        int GetKeySize();
        byte[] GetKey();
        int GetDefaultIteration();
       

    }
}
