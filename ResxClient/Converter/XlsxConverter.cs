using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ResourceManager.Exceptions;
using ResourceManager.Core;

namespace ResourceManager.Converter
{
    public class XlsxConverter : ConverterBase, ResourceManager.Converter.IConverter
    {
        private const string COMMENTS_KEY = "[Comments]";
        private double ColumnValueWidth = 40;
        private double ColumnCommentWidth = 40;


        public XlsxConverter(VSSolution solution)
            : base(solution)
        {
        }
        public XlsxConverter(IEnumerable<VSProject> projects)
            : base(projects)
        {
        }
        public XlsxConverter(IEnumerable<IResourceFileGroup> fileGroups, VSSolution solution)
            : base(fileGroups, solution)
        {
        }
        public XlsxConverter(VSProject project)
            : base(project)
        {
        }

        public void Export(string filePath)
        {
            using (var workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                IEnumerable<CultureInfo> cultures = null;
                if (Cultures != null)
                    cultures = Cultures.Select(vc => vc.Culture);
                else
                    cultures = Solution.Cultures.Keys;

                IEnumerable<VSProject> projects = Projects;
                if (Projects == null)
                    projects = (IEnumerable<VSProject>)Solution.Projects.Values;

                foreach (var project in projects)
                {
                    var data = GetData(project, cultures);

                    if (IncludeProjectsWithoutTranslations || data.Count() > 0)
                        AddProject(project, workbook, cultures, data);
                }

                workbook.SaveAs(filePath);
            }
        }
        private IEnumerable<ResourceDataGroupBase> GetData(VSProject project, IEnumerable<CultureInfo> cultures)
        {
            var data = new List<ResourceDataGroupBase>();
            IList<ResourceDataGroupBase> uncompletedDataGroups = null;

            if (ExportDiff)
                uncompletedDataGroups = project.GetUncompleteDataGroups(cultures);
            
            IEnumerable<IResourceFileGroup> resxGroups = project.ResxGroups.Values;
            if (FileGroups != null && FileGroups.Count() > 0)
                resxGroups = project.ResxGroups.Values.Intersect(FileGroups);

            foreach (IResourceFileGroup group in resxGroups)
            {
                IEnumerable<ResourceDataGroupBase> groupDataValues = group.AllData.Values
                    .Where(resxGroup => uncompletedDataGroups == null || uncompletedDataGroups.Contains(resxGroup));

                if (IgnoreInternalResources)
                {
                    groupDataValues = groupDataValues.Where(resxGroup => !resxGroup.Name.StartsWith(">>"));
                }

                data.AddRange(groupDataValues);
            }

            return data;
        }
        private void AddProject(VSProject project, XLWorkbook workbook, IEnumerable<CultureInfo> cultures, IEnumerable<ResourceDataGroupBase> data)
        {
            var worksheet = workbook.Worksheets.Add(project.Name);

            AddHeader(worksheet, cultures);

            int rowIndex = 2;
            foreach (ResourceDataGroupBase dataGroup in data)
            {
                AddData(dataGroup, worksheet, rowIndex, cultures);
                rowIndex++;
            }

            if (AutoAdjustLayout)
            {
                worksheet.Row(1).Style.Font.SetBold(true);
                worksheet.Columns(1, 2).Style.Font.SetFontColor(XLColor.Gray);
                worksheet.Columns(1, 2).Width = 12.0;

                worksheet.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            }
        }
        private void AddHeader(IXLWorksheet worksheet, IEnumerable<CultureInfo> cultures)
        {
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Keys";            

            int c = 3;
            foreach (var culture in cultures)
            {
                if (AutoAdjustLayout)
                {
                    worksheet.Column(c).Width = ColumnValueWidth;
                    worksheet.Column(c).Style.Alignment.SetWrapText(true);
                }
                worksheet.Cell(1, c++).Value = culture.Name;


                if (ExportComments)
                {
                    if (AutoAdjustLayout)
                    {
                        worksheet.Column(c).Width = ColumnCommentWidth;
                        worksheet.Column(c).Style.Alignment.SetWrapText(true);
                    }
                    worksheet.Cell(1, c++).Value = culture.Name + " " + COMMENTS_KEY;
                }
            }
        }

        private void AddData(ResourceDataGroupBase dataGroup, IXLWorksheet worksheet, int rowIndex, IEnumerable<CultureInfo> cultures)
        {
            worksheet.Cell(rowIndex, 1).Value = dataGroup.FileGroup.ID;
            worksheet.Cell(rowIndex, 2).Value = dataGroup.Name;

            int c = 3;
            foreach (var culture in cultures)
            {
                if (dataGroup.ResxData.ContainsKey(culture))
                {
                    worksheet.Cell(rowIndex, c++).Value = dataGroup.ResxData[culture].Value;

                    if (ExportComments)
                    {
                        worksheet.Cell(rowIndex, c++).Value = dataGroup.ResxData[culture].Comment;
                    }
                }
                else
                {
                    worksheet.Cell(rowIndex, c++).Value = "";
                    if (ExportComments)
                        worksheet.Cell(rowIndex, c++).Value = "";
                }
            }
        }

        public int Import(string filePath)
        {
            int count = 0;

            using (var workbook = new XLWorkbook(filePath, XLEventTracking.Disabled))
            {
                foreach (var worksheet in workbook.Worksheets)
                {
                    string projectName = worksheet.Name;
                    if (!Solution.Projects.ContainsKey(projectName))
                        throw new ProjectUnknownException(projectName);

                    var project = Solution.Projects[projectName];
                    var translations = TranslationRow.LoadRows(worksheet);

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
                                project.ResxGroups[t.ID].SetResourceData(t.Key, te.Value, te.Key);
                                count++;
                            }
                            else if (dataGroup.ResxData[te.Key].Value != te.Value)
                            {
                                dataGroup.ResxData[te.Key].Value = te.Value;
                                count++;
                            }
                        }
                        foreach (var te in t.Comments)
                        {
                            if (!dataGroup.ResxData.ContainsKey(te.Key))
                            {
                                project.ResxGroups[t.ID].SetResourceDataComment(t.Key, te.Value, te.Key);
                                count++;
                            }
                            else if (dataGroup.ResxData[te.Key].Comment != te.Value)
                            {
                                dataGroup.ResxData[te.Key].Comment = te.Value;
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        

        protected class TranslationRow
        {
            public string ID { get; set; }
            public string Key { get; set; }

            private Dictionary<CultureInfo, string> translations = new Dictionary<CultureInfo, string>();
            private Dictionary<CultureInfo, string> comments = new Dictionary<CultureInfo, string>();
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
            public Dictionary<CultureInfo, string> Comments
            {
                get
                {
                    return comments;
                }
            }

            public static List<TranslationRow> LoadRows(IXLWorksheet worksheet)
            {
                List<TranslationRow> result = new List<TranslationRow>();

                var cultures = ReadCultures(worksheet);
                var commentColumnIndexes = cultures.Select(x => x.CommentColumnIndex);
                var textColumnIndexes = cultures.Select(x => x.TextColumnIndex);
                int lastColumn = commentColumnIndexes.Concat(textColumnIndexes).Max(x => x);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var textValues = row.Cells(1, lastColumn + 1).Select(cell => (cell.Value != null ? cell.Value.ToString() : null)).ToList<String>();
                
                    if (textValues.Count() > 0)
                    {
                        var customer = new TranslationRow();
                        customer.ID = textValues[0];
                        customer.Key = textValues[1];

                        foreach(var culture in cultures) 
                        {
                            if (culture.TextColumnIndex > 0 && culture.TextColumnIndex < textValues.Count &&
                                !String.IsNullOrWhiteSpace(textValues[culture.TextColumnIndex]))
                                customer.Translations.Add(culture.Culture, textValues[culture.TextColumnIndex]);
                            if (culture.CommentColumnIndex > 0 && culture.CommentColumnIndex < textValues.Count
                                && !String.IsNullOrWhiteSpace(textValues[culture.CommentColumnIndex]))
                                customer.Comments.Add(culture.Culture, textValues[culture.CommentColumnIndex]);
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
            public static IList<TranslationColumn> ReadCultures(IXLWorksheet worksheet)
            {
                var textValues = worksheet.Row(1).Cells().Where(cell => cell.Value != null).Select(cell => cell.Value.ToString()).ToList<String>();
                var cols = textValues.Skip(2).Where(s => !s.Contains(COMMENTS_KEY)).ToList<String>();

                var list = new List<TranslationColumn>();
                foreach (var s in cols)
                {
                    var textColumn = new TranslationColumn(new CultureInfo(s));
                    textColumn.TextColumnIndex = textValues.IndexOf(s);

                    string commentsKey = s;
                    string commentColumn = null;
                    
                    commentsKey += " [Comments]";
                    commentColumn = textValues.Skip(2).Where(t => t.Equals(commentsKey)).FirstOrDefault();  
                    
                    if(commentColumn == null && String.IsNullOrEmpty(s))
                        commentColumn = textValues.Skip(2).Where(t => t.Equals(COMMENTS_KEY)).FirstOrDefault();  

                    if (commentColumn != null)
                        textColumn.CommentColumnIndex = textValues.IndexOf(commentColumn);

                    list.Add(textColumn);
                }

                return list;
            }
            public static int GetColumnsCount(IEnumerable<TranslationColumn> list)
            {
                int count = 0;

                foreach (var column in list)
                {
                    if (column.TextColumnIndex > -1)
                        count++;
                    if (column.CommentColumnIndex > -1)
                        count++;
                }

                return count;
            }
        }

        protected class TranslationColumn
        {
            public TranslationColumn(CultureInfo culture)
            {
                Culture = culture;
                TextColumnIndex = -1;
                CommentColumnIndex = -1;
            }

            public CultureInfo Culture
            {
                get;
                private set;
            }
            public int TextColumnIndex
            {
                get;
                set;
            }
            public int CommentColumnIndex
            {
                get;
                set;
            }
        }
    }
}
