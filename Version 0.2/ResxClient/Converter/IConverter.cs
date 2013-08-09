using System;
namespace ResourceManager.Converter
{
    public interface IConverter
    {
        void Import(string filePath);
    }
}
