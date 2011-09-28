using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace ResourcenManager.Core
{
    public class VSResxDataGroup
    {
        private Dictionary<CultureInfo, VSResxData> data = new Dictionary<CultureInfo,VSResxData>();
        private string name;

        public VSResxDataGroup(string name)
        {
            this.name = name;
        }       
	
        public Dictionary<CultureInfo, VSResxData> ResxData
        {
            get { return data; }
        }
        public string Name
        {
            get { return name; }
        }
        public void Add(VSResxData resxdata)
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
