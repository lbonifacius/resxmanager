using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;
using ResourceManager.Exceptions;

namespace ResourceManager.Converter
{
    public static class ConverterFactory
    {
        public static IConverter OpenConverter(FileDialog openExcelDialog, VSSolution solution)
        {
            if (openExcelDialog == null)
                throw new ArgumentNullException("openExcelDialog");
            if(solution == null)
                throw new ArgumentNullException("solution");

            switch (Path.GetExtension(openExcelDialog.FileName).Substring(1))
            {
                case "xls":
                    return new ExcelConverter(solution);
                case "xlsx":
                    return new XlsxConverter(solution);
                default:
                    throw new ConverterFileTypeUnknownException();
            }
        }
        public static IConverter OpenConverter(FileDialog openExcelDialog, VSProject project)
        {
            if (openExcelDialog == null)
                throw new ArgumentNullException("openExcelDialog");
            if (project == null)
                throw new ArgumentNullException("project");

            switch (Path.GetExtension(openExcelDialog.FileName).Substring(1))
            {
                case "xls":
                    return new ExcelConverter(project);
                case "xlsx":
                    return new XlsxConverter(project);
                default:
                    throw new ConverterFileTypeUnknownException();
            }
        }
    }
}
