using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class AttributeItem
    {
        bool readOnly;

        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        private bool required;

        public bool Required
        {
            get { return required; }
            set { required = value; }
        }

        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public AttributeItem(object value) 
        {
            this.value = value;
        }

        public AttributeItem()
        {
            
        }
    }
}
