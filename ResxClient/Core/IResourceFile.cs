using System;
namespace ResourcenManager.Core
{
    public interface IResourceFile : IProjectFileSettings
    {
        System.Globalization.CultureInfo Culture { get; set; }
        bool IsCultureAutoDetected { get; }
        System.Collections.Generic.Dictionary<string, ResourceDataBase> Data { get; set; }
        System.IO.FileInfo File { get; }
        string ID { get; }
        string Prefix { get; }
        IResourceFileGroup FileGroup { get; }
        void CreateResourceData(string name, string value);
        void Save();
        void SetFileGroup(IResourceFileGroup group);
    }
}
