using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ResourceManager.Converter.Exceptions;
using ResourceManager.Core;

namespace ResourceManager.Converter
{
    public class XlsxConverter : ConverterBase, ResourceManager.Converter.IConverter
    {
        public XlsxConverter(VSSolution solution)
            : base(solution)
        {
        }
        public XlsxConverter(VSProject project)
            : base(project)
        {
        }

        public void Export(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart1 = package.AddWorkbookPart();

                Workbook workbook1 = new Workbook();
                workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                workbookPart1.Workbook = workbook1;

                var sheets = new Sheets();
                workbook1.Append(sheets);

                if (Project == null)
                {
                    uint i = 1;
                    foreach (var project in Solution.Projects.Values)
                    {
                        WorksheetPart worksheetPart = CreateSheet(i, project.Name, workbookPart1, sheets);

                        AddProject(project, worksheetPart);
                        i++;
                    }
                }
                else
                {
                    WorksheetPart worksheetPart = CreateSheet(1, Project.Name, workbookPart1, sheets);
                    AddProject(Project, worksheetPart);
                }
            }
        }

        private WorksheetPart CreateSheet(uint index, string name, WorkbookPart workbookPart, Sheets sheets)
        {
            WorksheetPart worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>("rId" + index);

            Sheet sheet1 = new Sheet() { Name = name, SheetId = UInt32Value.FromUInt32(index), Id = "rId" + index };
            sheets.Append(sheet1);

            return worksheetPart1;
        }

        private void AddProject(VSProject project, WorksheetPart worksheetPart)
        {
            Worksheet worksheet1 = new Worksheet();
            SheetData sheetData1 = new SheetData();

            AddHeader(project, sheetData1);

            IList<ResourceDataGroupBase> uncompletedDataGroups = null;

            if (ExportDiff)
            {
                uncompletedDataGroups = project.GetUncompleteDataGroups();
            }
            foreach (IResourceFileGroup group in project.ResxGroups.Values)
            {
                foreach (ResourceDataGroupBase dataGroup in group.AllData.Values
                    .Where(resxGroup => uncompletedDataGroups == null || uncompletedDataGroups.Contains(resxGroup)))
                {
                    AddData(group, dataGroup, sheetData1);
                }
            }

            worksheet1.Append(sheetData1);
            worksheetPart.Worksheet = worksheet1;
        }
        private void AddHeader(VSProject project, SheetData sheetData1)
        {
            Row row1 = new Row();
            AddInlineStringCell(row1, "ID");
            AddInlineStringCell(row1, "Keys");

            foreach (var culture in Solution.Cultures.Keys)
            {
                AddInlineStringCell(row1, culture.Name);

                if (ExportComments)
                    AddInlineStringCell(row1, culture.Name + "[Comments]");
            }

            sheetData1.Append(row1);
        }

        private void AddData(IResourceFileGroup group, ResourceDataGroupBase dataGroup, SheetData sheetData1)
        {
            Row row1 = new Row();
            AddInlineStringCell(row1, group.ID);
            AddInlineStringCell(row1, dataGroup.Name);

            foreach (var culture in Solution.Cultures.Keys)
            {
                if (dataGroup.ResxData.ContainsKey(culture))
                {
                    AddInlineStringCell(row1, dataGroup.ResxData[culture].Value);
                    if (ExportComments)
                        AddInlineStringCell(row1, dataGroup.ResxData[culture].Comment);
                }
                else
                {
                    AddInlineStringCell(row1, "");
                    if (ExportComments)
                        AddInlineStringCell(row1, "");
                }
            }

            sheetData1.Append(row1);
        }

        private void AddInlineStringCell(Row row1, string text)
        {
            var cell1 = new Cell() { DataType = CellValues.InlineString };
            var inlineString1 = new InlineString();
            inlineString1.Append(new Text(text));
            cell1.Append(inlineString1);
            row1.Append(cell1);
        }


        public void Import(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Open(filePath, false))
            {
                var workBook = package.WorkbookPart.Workbook;
                var workSheets = workBook.Descendants<Sheet>();
                var sharedStrings = package.WorkbookPart.SharedStringTablePart.SharedStringTable;

                foreach (var worksheet in workSheets)
                {
                    string projectName = worksheet.Name;
                    if (!Solution.Projects.ContainsKey(projectName))
                        throw new ProjectUnknownException(projectName);

                    VSProject project = Solution.Projects[projectName];

                    var translations = TranslationRow.LoadRows(((WorksheetPart)package.WorkbookPart.GetPartById(worksheet.Id)).Worksheet,
                        sharedStrings);

                    foreach (var t in translations)
                    {
                        ResourceDataGroupBase dataGroup = null;
                        if (!project.ResxGroups[t.ID].AllData.ContainsKey(t.Key))
                        {
                            dataGroup = project.ResxGroups[t.ID].CreateDataGroup(t.Key);
                            project.ResxGroups[t.ID].AllData.Add(t.Key, dataGroup);
                        }
                        else
                            dataGroup = project.ResxGroups[t.ID].AllData[t.Key];

                        foreach (var te in t.Translations)
                        {
                            if (!dataGroup.ResxData.ContainsKey(te.Key))
                            {
                                project.ResxGroups[t.ID].SetResourceData(t.Key, te.Value, "", te.Key);
                            }
                            else
                            {
                                dataGroup.ResxData[te.Key].Value = te.Value;
                            }
                        }
                    }
                }
            }
        }



        public class TranslationRow
        {
            public string ID { get; set; }
            public string Key { get; set; }

            private Dictionary<CultureInfo, string> translations = new Dictionary<CultureInfo, string>();
            public string this[CultureInfo culture]
            {
                get
                {
                    return translations[culture];
                }
                set
                {
                    translations[culture] = value;
                }
            }
            public Dictionary<CultureInfo, string> Translations
            {
                get
                {
                    return translations;
                }
            }

            public static List<TranslationRow> LoadRows(Worksheet worksheet,
              SharedStringTable sharedString)
            {
                List<TranslationRow> result = new List<TranslationRow>();

                var cultures = TranslationRow.ReadCultures(worksheet.Descendants<Row>().First(r => r.RowIndex == 1), sharedString);

                IEnumerable<Row> dataRows =
                  from row in worksheet.Descendants<Row>()
                  where row.RowIndex > 1
                  select row;

                foreach (Row row in dataRows)
                {
                    //LINQ query to return the row's cell values.
                    //Where clause filters out any cells that do not contain a value.
                    //Select returns the value of a cell unless the cell contains
                    //  a Shared String.
                    //If the cell contains a Shared String, its value will be a 
                    //  reference id which will be used to look up the value in the 
                    //  Shared String table.
                    IEnumerable<String> textValues =
                      from cell in row.Descendants<Cell>()
                      where cell.CellValue != null
                      select
                        (cell.DataType != null
                          && cell.DataType.HasValue
                          && cell.DataType == CellValues.SharedString
                        ? sharedString.ChildElements[
                          int.Parse(cell.CellValue.InnerText)].InnerText
                        : cell.CellValue.InnerText)
                      ;

                    //Check to verify the row contained data.
                    if (textValues.Count() > 0)
                    {
                        //Create a customer and add it to the list.
                        var textArray = textValues.ToArray();
                        var customer = new TranslationRow();
                        customer.ID = textArray[0];
                        customer.Key = textArray[1];

                        for (int i = 2; i < textArray.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(textArray[i]))
                                customer[cultures[i - 2]] = textArray[i];
                        }
                        result.Add(customer);
                    }
                    else
                    {
                        break;
                    }
                }

                return result;
            }

            public static List<CultureInfo> ReadCultures(Row row, SharedStringTable sharedString)
            {
                IEnumerable<String> textValues =
                    from cell in row.Descendants<Cell>()
                    where cell.CellValue != null
                    select
                    (cell.DataType != null
                        && cell.DataType.HasValue
                        && cell.DataType == CellValues.SharedString
                    ? sharedString.ChildElements[
                        int.Parse(cell.CellValue.InnerText)].InnerText
                    : cell.CellValue.InnerText)
                    ;

                if (textValues.Count() > 0)
                {
                    return textValues.Skip(2).Select(s => new CultureInfo(s))
                        .ToList<CultureInfo>();
                }
                else
                {
                    throw new Exception("No cultures found!");
                }
            }

        }
    }
}
