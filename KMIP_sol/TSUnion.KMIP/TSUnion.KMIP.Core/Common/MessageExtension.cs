using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Common
{
    [Serializable]
    public class MessageExtensionStructure
    {
        [Encoding(TagType.VendorIdentification, DataType.TextString)]
        public string VendorIdentification { get; set; }

        [Encoding(TagType.CriticalityIndicator, DataType.Boolean)]
        public Boolean CriticalityIndicator { get; set; }

        [Encoding(TagType.VendorExtension, DataType.Structure)]
        public VendorExtensionType VendorExtension { get; set; }

    }
}
