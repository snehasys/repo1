using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [AttributeUsage(AttributeTargets.Property )]
   public class EncodingAttribute : Attribute
    {
        TagType tag;

        public TagType Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        DataType data;

        public DataType Data
        {
            get { return data; }
            set { data = value; }
        }
        Boolean required;

        public Boolean Required
        {
            get { return required; }
            set { required = value; }
        }
        public EncodingAttribute(TagType tag, DataType data, Boolean required) 
        {
            this.tag = tag;
            this.data = data;
            this.required = required;
        }
        public EncodingAttribute(TagType tag, DataType data)
        {
            this.tag = tag;
            this.data = data;
            this.required = false;
        }

    }
}
