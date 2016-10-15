using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class CryptographicParameters
    {
        public BlockCipherModeType BlockCipherMode { get; set; }
        public PaddingMethodType PaddingMethod  { get; set; }
        public HashingAlgorithmType HashingAlgorithm  { get; set; }
        public KeyRoleType KeyRole { get; set; }

        public static CryptographicParameters Create() 
        {
            return new CryptographicParameters();
        }
    }
}
