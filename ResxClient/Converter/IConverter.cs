using System;
namespace ResourceManager.Converter
{
    public interface IConverter
    {
        int Import(string filePath);
    }
}
