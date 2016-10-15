using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Attributes
{
    [Serializable]
    public class ManagedAttribute
    {
        public struct Name
        {
            NameType Type;
            String Value;
        }

        Dictionary<string, object> attributes = new Dictionary<string, object>();

        public Dictionary<string, object> Items
        {
            get { return attributes; }
            set { attributes = value; }
        }
    }
}
