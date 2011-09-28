using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ResourcenManager.Core
{
    public class VSCulture
    {
        private CultureInfo culture;
        private List<VSResxFile> files = new List<VSResxFile>();

        public VSCulture(CultureInfo culture)
        {
            this.culture = culture;
        }

        public CultureInfo Culture
        {
            get
            {
                return culture;
            }
        }

        public List<VSResxFile> Files
        {
            get
            {
                return files;
            }
        }

        public List<VSResxData> GetItemsNotExistingInCulture(VSCulture culture)
        {
            List<VSResxData> notExisting = new List<VSResxData>();

            foreach (VSResxFile file in files)
            {
                foreach (VSResxData data in file.Data.Values)
                {
                    if (data.ResxFile.ResxFileGroup.ResxFiles.ContainsKey(culture.Culture))
                    {
                        VSResxFile culturedFile = data.ResxFile.ResxFileGroup.ResxFiles[culture.Culture];

                        if (!culturedFile.Data.ContainsKey(data.Name))
                        {
                            notExisting.Add(data);
                        }
                    }
                    else
                    {
                        notExisting.Add(data);
                    }
                }
            }

            return notExisting;
        }    
    }
}
