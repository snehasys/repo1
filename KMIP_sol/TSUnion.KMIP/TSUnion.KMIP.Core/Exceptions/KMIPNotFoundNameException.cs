﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    class KMIPNotFoundNameException : Exception
    {
        public KMIPNotFoundNameException(string msg) : base(msg) { }
    }
}
