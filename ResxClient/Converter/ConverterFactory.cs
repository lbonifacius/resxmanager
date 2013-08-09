using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Converter
{
    public class ConverterFactory
    {
        public static IConverter OpenConverter(OpenFileDialog openExcelDialog, VSSolution solution)
        {
            switch (Path.GetExtension(openExcelDialog.FileName).Substring(1))
            { 
                case "xls":
                    return new ExcelConverter(solution);
                case "xlsx":
                    return new XlsxConverter(solution);
                default:
                    throw new Exception("File type not supported!");
            }
        }
        public static IConverter OpenConverter(OpenFileDialog openExcelDialog, VSProject project)
        {
            switch (Path.GetExtension(openExcelDialog.FileName).Substring(1))
            {
                case "xls":
                    return new ExcelConverter(project);
                case "xlsx":
                    return new XlsxConverter(project);
                default:
                    throw new Exception("File type not supported!");
            }
        }
    }
}
