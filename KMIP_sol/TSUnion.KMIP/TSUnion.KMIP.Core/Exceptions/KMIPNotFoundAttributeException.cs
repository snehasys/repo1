using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    class KMIPNotFoundAttributeException : Exception
    {
        public KMIPNotFoundAttributeException(string message) : base(message) { }

    }
}
