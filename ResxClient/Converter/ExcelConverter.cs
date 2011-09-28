using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Linq;
using ResourcenManager.Converter.Exceptions;

namespace ResourcenManager.Core
{
    public class ExcelConverter
    {
        string urnschemasmicrosoftcomofficespreadsheet = "urn:schemas-microsoft-com:office:spreadsheet";

        int expandedColumnCount = 0;
        int expandedRowCount = 0;

        public ExcelConverter(VSSolution solution)
        {
            this.Solution = solution;
        }
        public ExcelConverter(VSProject project)
        {
            this.Project = project;
            this.Solution = project.Solution;
        }

        public VSProject Project
        {
            get;
            private set;
        }
        public VSSolution Solution
        {
            get;
            private set;
        }

        public XmlDocument Export()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ResxClient.Templates.EmptyExcelSheet.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(stream);


            //XmlDocument xml = new XmlDocument();            
            //XmlProcessingInstruction processing = xml.CreateProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            //xml.AppendChild(processing);

            //xml.AppendChild(xml.CreateElement("Workbook"));

            //xml.DocumentElement.SetAttribute("xmlns", "urn:schemas-microsoft-com:office:spreadsheet");
            //xml.DocumentElement.SetAttribute("xmlns:o", "urn:schemas-microsoft-com:office:office");
            //xml.DocumentElement.SetAttribute("xmlns:x", "urn:schemas-microsoft-com:office:excel");
            //xml.DocumentElement.SetAttribute("xmlns:ss", "urn:schemas-microsoft-com:office:spreadsheet");
            //xml.DocumentElement.SetAttribute("xmlns:html", "http://www.w3.org/TR/REC-html40");

            //XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xml.NameTable);
            //namespaceManager.AddNamespace("", "urn:schemas-microsoft-com:office:spreadsheet");
            //namespaceManager.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            //namespaceManager.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            //namespaceManager.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
            //namespaceManager.AddNamespace("html", "http://www.w3.org/TR/REC-html40");


            //XmlElement worksheet = xml.DocumentElement.ChildNodes[3] as XmlElement; // TODO

            if (Project == null)
            {
                foreach (VSProject project in Solution.Projects.Values)
                {
                    if(project.ResxGroups.Count > 0)
                        AddProject(xml.DocumentElement, project);
                }
            }
            else
                AddProject(xml.DocumentElement, Project);

            return xml;
        }

        private void AddProject(XmlElement docElement, VSProject project)
        {
            IList<VSResxDataGroup> uncompletedDataGroups = null;

            if (ExportDiff)
            {
                uncompletedDataGroups = project.GetUncompleteDataGroups();

                if (uncompletedDataGroups.Count == 0)
                    return;
            }

            XmlElement worksheet = docElement.OwnerDocument.CreateElement("Worksheet", urnschemasmicrosoftcomofficespreadsheet);
            SetAttribute(worksheet, "ss", "Name", "urn:schemas-microsoft-com:office:spreadsheet", project.Name);
            docElement.AppendChild(worksheet);

            XmlElement table = worksheet.OwnerDocument.CreateElement("Table", urnschemasmicrosoftcomofficespreadsheet);
            worksheet.AppendChild(table);

            XmlElement row = AddRow(table);
            AddCell(row, 1, "ID");
            AddCell(row, 2, "Keys");

            int i = 3;
            foreach (VSCulture culture in project.Solution.Cultures.Values)
            {
                AddCell(row, i, culture.Culture.Name, "s21");
                i++;
            }

            foreach (VSResxFileGroup group in project.ResxGroups.Values)
            {
                foreach (VSResxDataGroup dataGroup in group.AllData.Values
                    .Where(resxGroup => uncompletedDataGroups == null || uncompletedDataGroups.Contains(resxGroup)))
                {
                    XmlElement datarow = AddRow(table);
                    AddCell(datarow, 1, group.ID);
                    AddCell(datarow, 2, dataGroup.Name);

                    i = 3;
                    foreach (VSCulture culture in project.Solution.Cultures.Values)
                    {
                        if (dataGroup.ResxData.ContainsKey(culture.Culture))
                            AddCell(datarow, i, dataGroup.ResxData[culture.Culture].Value);

                        i++;
                    }
                }
            }

            SetAttribute(table, "ss", "ExpandedColumnCount", "urn:schemas-microsoft-com:office:spreadsheet", expandedColumnCount.ToString());
            SetAttribute(table, "ss", "ExpandedRowCount", "urn:schemas-microsoft-com:office:spreadsheet", expandedRowCount.ToString());
            SetAttribute(table, "x", "FullColumns", "urn:schemas-microsoft-com:office:excel", "1");
            SetAttribute(table, "x", "FullRows", "urn:schemas-microsoft-com:office:excel", "1");
            SetAttribute(table, "ss", "DefaultColumnWidth", "urn:schemas-microsoft-com:office:spreadsheet", "100");
        }

        private XmlElement AddRow(XmlElement table)
        {
            XmlElement row = table.OwnerDocument.CreateElement("Row", urnschemasmicrosoftcomofficespreadsheet);
            table.AppendChild(row);

            return row;
        }
        private void AddCell(XmlElement row, int index, object value)
        {
            AddCell(row, index, value, null);
        }
        private void AddCell(XmlElement row, int index, object value, string stylename)
        {
            XmlElement cell = row.OwnerDocument.CreateElement("Cell", urnschemasmicrosoftcomofficespreadsheet);
            SetAttribute(cell, "ss", "Index", "urn:schemas-microsoft-com:office:spreadsheet", index.ToString());
            if (stylename != null && stylename != "")
                SetAttribute(cell, "ss", "StyleID", "urn:schemas-microsoft-com:office:spreadsheet", stylename);

            row.AppendChild(cell);
            XmlElement data = row.OwnerDocument.CreateElement("Data", urnschemasmicrosoftcomofficespreadsheet);
            SetAttribute(data, "ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet", "String");
            data.InnerText = value.ToString();
            cell.AppendChild(data);

            if (index >= expandedColumnCount)
                expandedColumnCount = index;
        }
        private void SetAttribute(XmlElement element, string namespacePrefix, string name, string namespaceName, string value)
        {
            XmlAttribute xattrs = element.OwnerDocument.CreateAttribute(namespacePrefix, name, namespaceName);
            String nuri = xattrs.NamespaceURI;
            xattrs.Value = value;

            element.Attributes.Append(xattrs);

            expandedRowCount++;
        }

        public void Import(string filename)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filename);

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xml.NameTable);
            namespaceManager.AddNamespace("", "urn:schemas-microsoft-com:office:spreadsheet");
            namespaceManager.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            namespaceManager.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            namespaceManager.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
            namespaceManager.AddNamespace("html", "http://www.w3.org/TR/REC-html40");

            List<VSCulture> cultures = new List<VSCulture>();

            XmlNodeList worksheets = xml.SelectNodes("/ss:Workbook/ss:Worksheet", namespaceManager);

            foreach (XmlNode worksheet in worksheets)
            {
                string projectName = worksheet.SelectSingleNode("@ss:Name", namespaceManager).Value;
                if(!Solution.Projects.ContainsKey(projectName))
                    throw new ProjectUnknownException(projectName);

                VSProject project = Solution.Projects[projectName];

                XmlNodeList nodes = worksheet.SelectNodes("ss:Table/ss:Row", namespaceManager);
                foreach (XmlNode rowNode in nodes)
                {
                    if (rowNode == rowNode.ParentNode.SelectSingleNode("ss:Row", namespaceManager))
                    {
                        XmlNodeList cellNodes = rowNode.SelectNodes("ss:Cell", namespaceManager);
                        for (int i = 2; i < cellNodes.Count; i++)
                        {
                            cultures.Add(new VSCulture(CultureInfo.GetCultureInfo(cellNodes[i].FirstChild.InnerText)));
                        }
                    }
                    else
                    {
                        string key = rowNode.ChildNodes[1].FirstChild.InnerText;
                        string id = rowNode.FirstChild.FirstChild.InnerText;

                        VSResxDataGroup dataGroup = null;
                        if (!project.ResxGroups[id].AllData.ContainsKey(key))
                        {
                            dataGroup = new VSResxDataGroup(key);
                            project.ResxGroups[id].AllData.Add(key, dataGroup);
                        }
                        else
                            dataGroup = project.ResxGroups[id].AllData[key];

                        for (int i = 0; i < cultures.Count; i++)
                        {
                            XmlNode valueNode = rowNode.SelectSingleNode("ss:Cell[@ss:Index = '" + (i + 3) + "']/ss:Data", namespaceManager);
                            if (valueNode == null)
                                valueNode = rowNode.SelectSingleNode("ss:Cell[count(@ss:Index) = 0][" + (i + 3) + "]/ss:Data", namespaceManager);

                            if (valueNode != null)
                            {
                                if (!dataGroup.ResxData.ContainsKey(cultures[i].Culture))
                                {
                                    VSResxFile file = null;
                                    if (project.ResxGroups[id].ResxFiles.ContainsKey(cultures[i].Culture))
                                    {
                                        file = project.ResxGroups[id].ResxFiles[cultures[i].Culture];
                                        //dataGroup.ResxData.Add(cultures[i].Culture, new VSResxData(file));
                                    }
                                    else
                                    {
                                        file = project.ResxGroups[id].CreateNewFile(cultures[i].Culture);
                                    }


                                    file.CreateVSResxData(key, valueNode.InnerText);
                                }
                                else
                                {
                                    dataGroup.ResxData[cultures[i].Culture].Value = valueNode.InnerText;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool ExportDiff 
        { 
            get; 
            set; 
        }
    }
}
