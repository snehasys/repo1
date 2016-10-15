using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Attributes
{
    [Serializable]
    public class TemplateAttribute : ManagedAttribute
    {
        int attributeIndex;

        public int AttributeIndex
        {
            get { return attributeIndex; }
            set { attributeIndex = value; }
        }

    }
}
