using System;
namespace ResourceManager.Core
{
    public interface IResourceFile : IProjectFileSettings
    {
        System.Globalization.CultureInfo Culture { get; set; }
        bool IsCultureAutoDetected { get; }
        System.Collections.Generic.Dictionary<string, ResourceDataBase> Data { get; set; }
        System.IO.FileInfo File { get; }
        string ID { get; }
        string Prefix { get; }
        bool HasChanged { get; }
        IResourceFileGroup FileGroup { get; }
        void CreateResourceData(string name, string value);
        void SetResourceData(string name, string value);
        void CreateResourceDataComment(string name, string comment);
        void SetResourceDataComment(string name, string comment);
        void Save();
        void SetFileGroup(IResourceFileGroup group);
    }
}
