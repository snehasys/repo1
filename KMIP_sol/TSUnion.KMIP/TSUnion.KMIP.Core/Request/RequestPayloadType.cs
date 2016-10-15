using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Attributes;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Request
{
    [Serializable]
    public class RequestPayload
    {
        ObjectType type;

        public ObjectType Type
        {
            get { return type; }
            set { type = value; }
        }

        TemplateAttribute attribute;

        public TemplateAttribute Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }
    }
}
