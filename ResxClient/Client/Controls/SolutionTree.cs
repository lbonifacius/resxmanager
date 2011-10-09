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

namespace ResourceManager.Client.Controls
{
    public partial class SolutionTree : UserControl
    {
        private Core.VSSolution solution;
        private IList<CultureInfo> allCultures;

        public Core.VSSolution Solution
        {
            get { return solution; }
            set 
            { 
                solution = value;
                loadTree();            
            }
        }
	
        public SolutionTree()
        {
            InitializeComponent();

            allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList<CultureInfo>();

            this.contextMenuResxFile.Opening += new CancelEventHandler(contextMenuResxFile_Opening);
            this.contextMenuResxFile.Closed += new ToolStripDropDownClosedEventHandler(contextMenuResxFile_Closed);
            this.itemSetCulture.Text = Properties.Resources.SetCulture;

            this.itemExportToExcel.Text = Properties.Resources.ExportToExcel;
            this.itemImportFromExcel.Text = Properties.Resources.ImportFromExcel;

            this.itemRefreshAnalysis.Text = Properties.Resources.Refresh;
            this.itemRefreshAnalysis.Click += new EventHandler(itemRefreshAnalysis_Click);

            this.openFileDialog.Filter = Properties.Resources.ExcelFileFilter;
            this.saveFileDialog.Filter = Properties.Resources.ExcelFileFilter;
        }       

        void itemRefreshAnalysis_Click(object sender, EventArgs e)
        {
            this.treeView.Nodes[0].LastNode.Remove();
            loadCultures(this.treeView.Nodes[0]);
        }

        void cbCultureInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            IResourceFile file = ((ResxFileTreeNode)this.treeView.SelectedNode).ResxFile;
            file.Culture = CultureInfo.GetCultureInfo(((CulturesComboBoxItem)this.cbCultureInfos.SelectedItem).Name);

            file.FileGroup.Container.Project.ResxProjectFile.SaveFile(file);
            file.FileGroup.Container.Project.ResxProjectFile.Save();
        }

        void contextMenuResxFile_Opening(object sender, CancelEventArgs e)
        {
            this.cbCultureInfos.Items.Clear();

            var file = ((ResxFileTreeNode)this.treeView.SelectedNode).ResxFile;
            var otherUsedCultures = file.FileGroup.Files.Keys
                .Except(new CultureInfo[] { file.Culture });

            var list = allCultures.Except(otherUsedCultures);

            foreach (CultureInfo culture in list.OrderBy(c => c.DisplayName))
            {
                var item = new CulturesComboBoxItem(culture);
                this.cbCultureInfos.Items.Add(item);

                if (culture.Name == file.Culture.Name)
                    this.cbCultureInfos.SelectedItem = item;
            }

            this.cbCultureInfos.SelectedIndexChanged += new EventHandler(cbCultureInfos_SelectedIndexChanged);
        }
        void contextMenuResxFile_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.cbCultureInfos.SelectedIndexChanged -= new EventHandler(cbCultureInfos_SelectedIndexChanged);   
        }

        private void loadTree()
        {
            if (solution != null)
            {
                this.treeView.Invoke((MethodInvoker)(() => this.treeView.Nodes.Clear()));

                TreeNode slnNode = new TreeNode();
                slnNode.Text = solution.Name;
                this.treeView.Invoke((MethodInvoker)(() => treeView.Nodes.Add(slnNode)));

                loadProjects(slnNode);
                loadCultures(slnNode);

                this.treeView.Invoke((MethodInvoker)(() => slnNode.Expand()));
            }
        }
        private void loadProjects(TreeNode parent)
        {
            TreeNode projectsNode = new TreeNode();
            projectsNode.Text = Properties.Resources.Projects;            

            foreach (VSProject project in solution.Projects.Values)
            {
                ProjectTreeNode projectNode = new ProjectTreeNode();
                projectNode.Project = project;
                projectNode.ContextMenuStrip = contextMenuProject;
                projectsNode.Nodes.Add(projectNode);

                loadFiles(projectNode, project);
            }            
            
            treeView.Invoke((MethodInvoker)(() => parent.Nodes.Add(projectsNode)));
            treeView.Invoke((MethodInvoker)(() => projectsNode.Expand()));
        }
        private void loadFiles(TreeNode parent, VSProject project)
        {
            foreach (IResourceFileGroup group in project.ResxGroups.Values)
            {
                foreach (IResourceFile file in group.Files.Values)
                {
                    ResxFileTreeNode fileNode = new ResxFileTreeNode();
                    fileNode.ContextMenuStrip = this.contextMenuResxFile;
                    fileNode.ResxFile = file;
                    parent.Nodes.Add(fileNode);
                }
            }

            foreach (IResourceFile file in project.UnassignedFiles)
            {
                ResxFileTreeNode fileNode = new ResxFileTreeNode();
                fileNode.ContextMenuStrip = this.contextMenuResxFile;
                fileNode.ResxFile = file;
                parent.Nodes.Add(fileNode);
            }
        }
        private void loadCultures(TreeNode parent)
        {
            TreeNode culturesNode = new TreeNode();
            culturesNode.Text = Properties.Resources.Cultures;
            culturesNode.ContextMenuStrip = contextMenuAnalysis;

            foreach (VSCulture culture in solution.Cultures.Values)
            {
                TreeNode cultureNode = new TreeNode();
                cultureNode.Text = culture.Culture.DisplayName;
                culturesNode.Nodes.Add(cultureNode);

                loadNotExistings(cultureNode, culture);
            }

            treeView.Invoke((MethodInvoker)(() => parent.Nodes.Add(culturesNode)));
            treeView.Invoke((MethodInvoker)(() => culturesNode.Expand()));
        }
        private void loadNotExistings(TreeNode parent, VSCulture culture)
        {
            List<ResourceDataBase> notexisting = null;
            foreach (VSCulture targetCulture in solution.Cultures.Values)
            {
                if (targetCulture != culture)
                {
                    notexisting = culture.GetItemsNotExistingInCulture(targetCulture);

                    TreeNode notexistingNode = new TreeNode();
                    notexistingNode.Text = String.Format(Properties.Resources.NotExistingInLanguage, new object[] { notexisting.Count, targetCulture.Culture.DisplayName });
                    parent.Nodes.Add(notexistingNode);

                    loadDataItems(notexistingNode, notexisting);
                }
            }
        }
        private void loadDataItems(TreeNode parent, List<ResourceDataBase> items)
        {
            foreach (ResourceDataBase item in items)
            {
                TreeNode itemNode = new TreeNode();
                itemNode.Text = item.Name + ": " + item.Value;
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
            saveFileDialog.FileName = ((ProjectTreeNode)treeView.SelectedNode).Project.Name + ".xls";
            DialogResult result = saveFileDialog.ShowDialog();           

            if (result == DialogResult.OK)
            {
                ExcelConverter excel = new ExcelConverter(((ProjectTreeNode)treeView.SelectedNode).Project);
                excel.Export().Save(saveFileDialog.FileName);
            }
        }

        private void itemImportFromExcel_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExcelConverter excel = new ExcelConverter(((ProjectTreeNode)treeView.SelectedNode).Project);
            excel.Import(openFileDialog.FileName);
        }
    }
}
