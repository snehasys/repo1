using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Attributes;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Response
{
    [Serializable]
    public class ResponsePayloadType
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

        List<Guid> uniqueIdentifier;

        public List<Guid> UniqueIdentifier
        {
            get 
            {
                if (uniqueIdentifier == null) 
                {
                    uniqueIdentifier = new List<Guid>();
                }
                return uniqueIdentifier;
            }
          
        }
    }
}
