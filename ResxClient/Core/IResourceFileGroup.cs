using System;
using System.Globalization;
namespace ResourcenManager.Core
{
    public interface IResourceFileGroup
    {
        void Add(IResourceFile file);
        System.Collections.Generic.Dictionary<string, ResourceDataGroupBase> AllData { get; }
        VSFileContainer Container { get; }
        IResourceFile CreateNewFile(System.Globalization.CultureInfo culture);
        ResourceDataGroupBase CreateDataGroup(string name);

        string FileGroupPath { get; }
        string ID { get; }
        string Prefix { get; }
        void RegisterResourceData(ResourceDataBase data);
        System.Collections.Generic.Dictionary<System.Globalization.CultureInfo, IResourceFile> Files { get; }

        void ChangeCulture(IResourceFile file, CultureInfo culture);
    }
}
