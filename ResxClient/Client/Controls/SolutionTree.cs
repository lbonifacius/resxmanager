using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;
using System.Globalization;
using System.Linq;
using ResourceManager.Storage;
using ResourceManager.Converter;

namespace ResourceManager.Client.Controls
{
    public partial class SolutionTree : UserControl
    {
        private IList<CultureInfo> allCultures;

        public MainForm Main
        {
            get;
            set;
        }

        public SolutionTree()
        {
            InitializeComponent();

            allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList<CultureInfo>();            

            this.contextMenuResxFile.Opening += new CancelEventHandler(contextMenuResxFile_Opening);
            this.contextMenuResxFile.Closed += new ToolStripDropDownClosedEventHandler(contextMenuResxFile_Closed);
            this.itemSetCulture.Text = Properties.Resources.SetCulture;

            this.exportProject.Text = Properties.Resources.ExportProjectToExcel;
            this.exportCulture.Text = Properties.Resources.ExportCulture;
            this.exportCulturePair.Text = Properties.Resources.ExportCulturePair;
            this.itemImportFromExcel.Text = Properties.Resources.ImportFromExcel;
            this.exportFileGroup.Text = Properties.Resources.ExportFileGroup;

            this.itemRefreshAnalysis.Text = Properties.Resources.Refresh;
            this.itemRefreshAnalysis.Click += new EventHandler(itemRefreshAnalysis_Click);

            this.openFileDialog.Filter = Properties.Resources.ExcelImportFileFilter;

            this.itemFill100PercMatches.Text = Properties.Resources.TranslateAuto;

            this.treeView.ImageList = this.iconImageList;
        }       

        void itemRefreshAnalysis_Click(object sender, EventArgs e)
        {
            refreshAnalysis();
        }
        private void refreshAnalysis()
        {
            for (int i = 0; i < treeView.Nodes[0].Nodes.Count - 1; i++)
            {
                foreach (ResourceFileGroupTreeNode fgnode in treeView.Nodes[0].Nodes[i].Nodes)
                {
                    refreshFiles(fgnode);
                }
            }

            this.treeView.Nodes[0].LastNode.Remove();
            loadCultures(this.treeView.Nodes[0]);
        }
        public void RefreshAnalysis()
        {
            Invoke((MethodInvoker)(() => refreshAnalysis()));
        }
        public void RefreshAnalysis(CultureAnalysisResultTreeNode node)
        {
            Invoke((MethodInvoker)(() => loadNotExistings(node)));
        }

        void cbCultureInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.setToolbarStatusText(Properties.Resources.ChangingCulture);

            var node = (ResourceFileTreeNode)this.treeView.SelectedNode;
            IResourceFile file = (node).File;
            file.Culture = CultureInfo.GetCultureInfo(((CulturesComboBoxItem)this.cbCultureInfos.SelectedItem).Name);

            file.FileGroup.Container.Project.ResxProjectFile.SaveFile(file);
            file.FileGroup.Container.Project.ResxProjectFile.Save();

            node.Refresh();
            Main.CurrentSolution.RemoveUnusedCultures();
            refreshAnalysis();

            Main.setToolbarStatusText(Properties.Resources.ChangingCultureCompleted, 4000);
        }

        void contextMenuResxFile_Opening(object sender, CancelEventArgs e)
        {
            this.cbCultureInfos.Items.Clear();

            var file = ((ResourceFileTreeNode)this.treeView.SelectedNode).File;
            var otherUsedCultures = file.FileGroup.Files.Keys
                .Except(new CultureInfo[] {file.Culture});

            var list = allCultures.Except(otherUsedCultures);

            foreach (CultureInfo culture in list.OrderBy(c => c.DisplayName))
            {                
                var item = new CulturesComboBoxItem(culture);
                this.cbCultureInfos.Items.Add(item);

                if(culture.Name == file.Culture.Name)
                    this.cbCultureInfos.SelectedItem = item;
            }

            this.cbCultureInfos.SelectedIndexChanged += new EventHandler(cbCultureInfos_SelectedIndexChanged);
        }
        void contextMenuResxFile_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.cbCultureInfos.SelectedIndexChanged -= new EventHandler(cbCultureInfos_SelectedIndexChanged);   
        }

        public void LoadTree()
        {
            if (Main.CurrentSolution != null)
            {
                this.treeView.Invoke((MethodInvoker)(() => this.treeView.Nodes.Clear()));

                TreeNode slnNode = new TreeNode();
                slnNode.Text = Main.CurrentSolution.Name;
                slnNode.ImageIndex = 0;
                slnNode.SelectedImageIndex = 0;
                this.treeView.Invoke((MethodInvoker)(() => treeView.Nodes.Add(slnNode)));

                loadProjects(slnNode);
                loadCultures(slnNode);

                this.treeView.Invoke((MethodInvoker)(() => slnNode.Expand()));
            }
            else
            {
                if (this.treeView.Nodes.Count > 0)
                    this.treeView.Nodes.Clear();
            }
        }
        private void loadProjects(TreeNode parent)
        {
            //TreeNode projectsNode = new TreeNode();
            //projectsNode.Text = Properties.Resources.Projects;            

            foreach (VSProject project in Main.CurrentSolution.Projects.Values)
            {
                ProjectTreeNode projectNode = new ProjectTreeNode();
                projectNode.Project = project;
                
                projectNode.ImageIndex = getProjectImageIndex(project.Type);
                projectNode.SelectedImageIndex = projectNode.ImageIndex;
                projectNode.ContextMenuStrip = contextMenuProject;               

                loadFileGroups(projectNode, project);

                treeView.Invoke((MethodInvoker)(() => parent.Nodes.Add(projectNode)));
            }            
            
            //treeView.Invoke((MethodInvoker)(() => parent.Nodes.Add(projectsNode)));
            //treeView.Invoke((MethodInvoker)(() => projectsNode.Expand()));
        }
        private int getProjectImageIndex(VSProjectTypes type)
        { 
            switch (type)
            { 
                case VSProjectTypes.CSharp:
                    return 6;
                case VSProjectTypes.FSharp:
                    return 7;
                case VSProjectTypes.VB:
                    return 8;
            }
            return 9;
        }
        private void loadFileGroups(TreeNode parent, VSProject project)
        {
            foreach (IResourceFileGroup group in project.ResxGroups.Values)
            {
                var fileGroupNode = new ResourceFileGroupTreeNode(group);
                fileGroupNode.ImageIndex = 1;
                fileGroupNode.SelectedImageIndex = 1;
                fileGroupNode.ContextMenuStrip = this.contextFileGroup;
                parent.Nodes.Add(fileGroupNode);

                loadFiles(fileGroupNode, group);
            }

            foreach (IResourceFile file in project.UnassignedFiles)
            {
                var fileNode = new ResourceFileTreeNode(file);
                fileNode.ContextMenuStrip = this.contextMenuResxFile;
                
                parent.Nodes.Add(fileNode);
            }
        }
        private void loadFiles(TreeNode parent, IResourceFileGroup group)
        {
            foreach (IResourceFile file in group.Files.Values)
            {
                var fileNode = new ResourceFileTreeNode(file);
                fileNode.ContextMenuStrip = this.contextMenuResxFile;
                fileNode.ImageIndex = 2;
                fileNode.SelectedImageIndex = 2;
                parent.Nodes.Add(fileNode);
            }
        }
        private void refreshFiles(ResourceFileGroupTreeNode parent)
        {
            var list = new List<IResourceFile>();

            foreach (ResourceFileTreeNode file in parent.Nodes)
            {
                list.Add(file.File);
            }

            foreach (IResourceFile file in parent.FileGroup.Files.Values.Except(list))
            {
                var fileNode = new ResourceFileTreeNode(file);
                fileNode.ContextMenuStrip = this.contextMenuResxFile;
                fileNode.ImageIndex = 2;
                fileNode.SelectedImageIndex = 2;
                parent.Nodes.Add(fileNode);
            }
        }
        private void loadCultures(TreeNode parent)
        {
            TreeNode culturesNode = new TreeNode();
            culturesNode.Text = Properties.Resources.Cultures;
            culturesNode.ContextMenuStrip = contextMenuAnalysis;
            culturesNode.ImageIndex = 3;
            culturesNode.SelectedImageIndex = 3;

            foreach (VSCulture culture in Main.CurrentSolution.Cultures.Values)
            {
                var cultureNode = new CultureTreeNode(culture);
                culturesNode.Nodes.Add(cultureNode);
                cultureNode.ImageIndex = 4;
                cultureNode.ContextMenuStrip = contextCulture;
                cultureNode.SelectedImageIndex = 4;

                loadNotExistings(cultureNode, culture);
            }

            treeView.Invoke((MethodInvoker)(() => parent.Nodes.Add(culturesNode)));
            treeView.Invoke((MethodInvoker)(() => culturesNode.Expand()));
        }
        private void loadNotExistings(TreeNode parent, VSCulture culture)
        {
            foreach (VSCulture targetCulture in Main.CurrentSolution.Cultures.Values)
            {
                if (targetCulture != culture)
                {
                    var notexistingNode = new CultureAnalysisResultTreeNode(culture, targetCulture);
                    parent.Nodes.Add(notexistingNode);

                    loadNotExistings(notexistingNode);
                }
            }
        }
        private void loadNotExistings(CultureAnalysisResultTreeNode node)
        {
            List<ResourceDataBase> notexisting = node.SourceCulture.GetItemsNotExistingInCulture(node.TargetCulture);
                        
            node.ContextMenuStrip = contextMenuAnalysisLang;
            node.Text = String.Format(Properties.Resources.NotExistingInLanguage, new object[] { notexisting.Count, node.TargetCulture.Culture.DisplayName });

            if (notexisting.Count > 0)
            {
                node.ForeColor = Color.Red;
                node.Parent.ForeColor = Color.Red;
            }

            loadDataItems(node, notexisting);
        }
        private void loadDataItems(TreeNode parent, List<ResourceDataBase> items)
        {
            parent.Nodes.Clear();

            foreach (ResourceDataBase item in items)
            {
                TreeNode itemNode = new TreeNode();
                itemNode.Text = item.Name + ": " + ((item.Value ?? "").Length <= 100 ? item.Value : (item.Value ?? "").Substring(0, 100) + " (...)");
                itemNode.ImageIndex = 5;
                itemNode.SelectedImageIndex = 5;
                parent.Nodes.Add(itemNode);
            }
        }

        private void itemSetCulture_Click(object sender, EventArgs e)
        {

        }

        private void cbCultureInfos_Click(object sender, EventArgs e)
        {
            
        }

        private void itemExportToExcel_Click(object sender, EventArgs e)
        {
            var excel = new ExcelExport();
            excel.SelectedProjects.Add(((ProjectTreeNode)treeView.SelectedNode).Project);
            excel.ShowDialog();
        }

        private void itemImportFromExcel_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            IConverter excel = ConverterFactory.OpenConverter(openFileDialog, ((ProjectTreeNode)treeView.SelectedNode).Project);
            excel.Import(openFileDialog.FileName);
        }

        private void itemFill100PercMatches_Click(object sender, EventArgs e)
        {
            var node = (CultureAnalysisResultTreeNode)treeView.SelectedNode;

            var task = Main.startNewTask(() => Main.fillTranslations(node));
        }

        private void exportFileGroup_Click(object sender, EventArgs e)
        {
            var node = (ResourceFileGroupTreeNode)treeView.SelectedNode;

            var excel = new ExcelExport();
            excel.SelectedFileGroups.Add(node.FileGroup);
            excel.Solution = Main.CurrentSolution;
            excel.ShowDialog();
        }

        private void exportCulturePair_Click(object sender, EventArgs e)
        {
            var node = (CultureAnalysisResultTreeNode)treeView.SelectedNode;

            var excel = new ExcelExport();
            excel.SelectedCultures.Add(node.SourceCulture);
            excel.SelectedCultures.Add(node.TargetCulture);
            excel.Solution = Main.CurrentSolution;
            excel.ShowDialog();
        }

        private void exportCulture_Click(object sender, EventArgs e)
        {
            var node = (CultureTreeNode)treeView.SelectedNode;

            var excel = new ExcelExport();
            excel.SelectedCultures.Add(node.Culture);
            excel.Solution = Main.CurrentSolution;
            excel.ShowDialog();
        }

    }
}
