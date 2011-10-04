using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ResourcenManager.Core
{
    public class VSCulture
    {
        private CultureInfo culture;
        private List<IResourceFile> files = new List<IResourceFile>();

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

        public List<IResourceFile> Files
        {
            get
            {
                return files;
            }
        }

        public List<ResourceDataBase> GetItemsNotExistingInCulture(VSCulture culture)
        {
            List<ResourceDataBase> notExisting = new List<ResourceDataBase>();

            foreach (IResourceFile file in files)
            {
                foreach (ResourceDataBase data in file.Data.Values)
                {
                    if (data.ResxFile.FileGroup.Files.ContainsKey(culture.Culture))
                    {
                        IResourceFile culturedFile = data.ResxFile.FileGroup.Files[culture.Culture];

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
