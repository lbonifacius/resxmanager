using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace ResourceManager.Core
{
    public class VSResxDataGroup : ResourceDataGroupBase
    {
        public VSResxDataGroup(string name, IResourceFileGroup fileGroup)
            : base(name, fileGroup)
        {
        } 	
    }
}
