using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class CredentialValue
    {
        public string Username { get; set;}
        public string Password { get; set; }
        public int GetSize() 
        {
            return Username.Count() +Password.Count();
        }
    }
}
