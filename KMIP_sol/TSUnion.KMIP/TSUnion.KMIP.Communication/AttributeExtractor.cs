using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TSUnion.KMIP.Core;

namespace TSUnion.KMIP.Communication
{
   public class AttributeExtractor
    {
        public  byte[] GetProperties(object obj)
        {
            Dictionary<string, string> _dict = new Dictionary<string, string>();

            PropertyInfo[] props = obj.GetType().GetProperties();
            
            foreach (PropertyInfo prop in props)
            {
                
                if (prop.PropertyType.IsClass) 
                {
                    PropertyInfo[] deriveObj = prop.GetType().GetProperties();

                }

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    EncodingAttribute authAttr = attr as EncodingAttribute;
                    if (authAttr != null)
                    {
                       
                    }
                }
            }

            return new byte[10];
        }
    }
}
