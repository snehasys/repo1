using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class CredentialStructure
    {
        [Encoding(TagType.CredentialValue, DataType.Structure)]
        public CredentialValue Value { get; set; }

        [Encoding(TagType.CredentialType, DataType.Enumeration)]
        public CredentialType Type { get; set; }

        public int GetSize() 
        {
            return sizeof(Int32) + Value.GetSize();
        }
    }
}
