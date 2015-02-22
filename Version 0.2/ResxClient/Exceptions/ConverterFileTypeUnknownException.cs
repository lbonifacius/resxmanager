using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManager.Exceptions
{
    public class ConverterFileTypeUnknownException : Exception
    {
        public ConverterFileTypeUnknownException()
            : base("File type unknown.")
        { }
    }
}
