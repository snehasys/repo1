using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TSUnion.KMIP.KmipUtil
{
    public static class XmlHelper
    {
        public static List<Guid> GetIdsFromXml(string path)
        {
            List<Guid> result = new List<Guid>();
            XDocument DocumentObject = XDocument.Load(path);
            IEnumerable<XElement> Itrem = from ItemInfo in DocumentObject.Descendants("ids").Elements("id") select ItemInfo;

            foreach (var t in Itrem)
            {
                Guid id = new Guid(t.Value.ToString());

                result.Add(id);

            }
            return result;
        }
    }
}
