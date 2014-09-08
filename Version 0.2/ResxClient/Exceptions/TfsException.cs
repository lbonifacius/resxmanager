using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManager.Exceptions
{
    public class TfsException : Exception
    {
        public TfsException(string msg)
            : base(String.Format(Properties.Resources.TFSException, msg))
        { }
    }
}
