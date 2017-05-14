using System;
using System.Globalization;
namespace ResourceManager.Core
{
    public interface IResourceFileGroup
    {
        void Add(IResourceFile file);
        System.Collections.Generic.Dictionary<string, ResourceDataGroupBase> AllData { get; }
        VSFileContainer Container { get; }
        IResourceFile CreateNewFile(System.Globalization.CultureInfo culture);
        ResourceDataGroupBase CreateDataGroup(string name);
        void SetResourceData(string key, string value, CultureInfo culture);
        void SetResourceDataComment(string key, string comment, CultureInfo culture);

        string FileGroupPath { get; }
        string ID { get; }
        string Prefix { get; }
        bool HasChanged { get; }
        void RegisterResourceData(ResourceDataBase data);
        System.Collections.Generic.Dictionary<System.Globalization.CultureInfo, IResourceFile> Files { get; }

        void ChangeCulture(IResourceFile file, CultureInfo culture);
    }
}
