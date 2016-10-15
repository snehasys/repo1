using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.DAO.MSSql
{
    [Serializable]
   public class FileKmipObject
    {
        public Guid ID { get; set; }
       public string Attributes { get; set; }
       public DateTime? Created { get; set; }
       public int Type { get; set; }
       public Binary Value { get; set; }
      


    }
}
