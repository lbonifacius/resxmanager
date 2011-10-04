using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace ResourceManager.Core
{
    public class ResourceDataGroupBase
    {
        private Dictionary<CultureInfo, ResourceDataBase> data = new Dictionary<CultureInfo, ResourceDataBase>();
        private string name;

        public ResourceDataGroupBase(string name)
        {
            this.name = name;
        }       
	
        public Dictionary<CultureInfo, ResourceDataBase> ResxData
        {
            get { return data; }
        }
        public string Name
        {
            get { return name; }
        }
        public void Add(ResourceDataBase resxdata)
        {
            if(resxdata.ResxFile.Culture != null)
                this.data.Add(resxdata.ResxFile.Culture, resxdata);
        }

        public bool IsComplete(IEnumerable<CultureInfo> requiredCultures)
        {
            return ResxData.Keys.Intersect(requiredCultures).Count() == requiredCultures.Count();
        }
    }
}
