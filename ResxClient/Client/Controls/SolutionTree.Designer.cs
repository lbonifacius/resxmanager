namespace ResourceManager.Client.Controls
{
    partial class SolutionTree
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionTree));
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuResxFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSetCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.cbCultureInfos = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemImportFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuAnalysis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRefreshAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAnalysisLang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportCulturePair = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemFill100PercMatches = new System.Windows.Forms.ToolStripMenuItem();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextFileGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportFileGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCulture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuResxFile.SuspendLayout();
            this.contextMenuProject.SuspendLayout();
            this.contextMenuAnalysis.SuspendLayout();
            this.contextMenuAnalysisLang.SuspendLayout();
            this.contextFileGroup.SuspendLayout();
            this.contextCulture.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(150, 150);
            this.treeView.TabIndex = 0;
            // 
            // contextMenuResxFile
            // 
            this.contextMenuResxFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSetCulture});
            this.contextMenuResxFile.Name = "contextMenuStrip1";
            this.contextMenuResxFile.Size = new System.Drawing.Size(181, 26);
            // 
            // itemSetCulture
            // 
            this.itemSetCulture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbCultureInfos});
            this.itemSetCulture.Name = "itemSetCulture";
            this.itemSetCulture.Size = new System.Drawing.Size(180, 22);
            this.itemSetCulture.Text = "toolStripMenuItem1";
            this.itemSetCulture.Click += new System.EventHandler(this.itemSetCulture_Click);
            // 
            // cbCultureInfos
            // 
            this.cbCultureInfos.Name = "cbCultureInfos";
            this.cbCultureInfos.Size = new System.Drawing.Size(250, 23);
            this.cbCultureInfos.Click += new System.EventHandler(this.cbCultureInfos_Click);
            // 
            // contextMenuProject
            // 
            this.contextMenuProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportProject,
            this.toolStripSeparator1,
            this.itemImportFromExcel});
            this.contextMenuProject.Name = "contextMenuProject";
            this.contextMenuProject.Size = new System.Drawing.Size(189, 54);
            // 
            // exportProject
            // 
            this.exportProject.Name = "exportProject";
            this.exportProject.Size = new System.Drawing.Size(188, 22);
            this.exportProject.Text = "exportProject";
            this.exportProject.Click += new System.EventHandler(this.itemExportToExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // itemImportFromExcel
            // 
            this.itemImportFromExcel.Name = "itemImportFromExcel";
            this.itemImportFromExcel.Size = new System.Drawing.Size(188, 22);
            this.itemImportFromExcel.Text = "itemImportFromExcel";
            this.itemImportFromExcel.Click += new System.EventHandler(this.itemImportFromExcel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Excel-Document|*.xlsx";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // contextMenuAnalysis
            // 
            this.contextMenuAnalysis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRefreshAnalysis});
            this.contextMenuAnalysis.Name = "contextMenuAnalysis";
            this.contextMenuAnalysis.Size = new System.Drawing.Size(181, 26);
            // 
            // itemRefreshAnalysis
            // 
            this.itemRefreshAnalysis.Name = "itemRefreshAnalysis";
            this.itemRefreshAnalysis.Size = new System.Drawing.Size(180, 22);
            this.itemRefreshAnalysis.Text = "itemRefreshAnalysis";
            // 
            // contextMenuAnalysisLang
            // 
            this.contextMenuAnalysisLang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCulturePair,
            this.toolStripSeparator2,
            this.itemFill100PercMatches});
            this.contextMenuAnalysisLang.Name = "contextMenuAnalysisLang";
            this.contextMenuAnalysisLang.Size = new System.Drawing.Size(169, 54);
            // 
            // exportCulturePair
            // 
            this.exportCulturePair.Name = "exportCulturePair";
            this.exportCulturePair.Size = new System.Drawing.Size(168, 22);
            this.exportCulturePair.Text = "exportCulturePair";
            this.exportCulturePair.Click += new System.EventHandler(this.exportCulturePair_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // itemFill100PercMatches
            // 
            this.itemFill100PercMatches.Name = "itemFill100PercMatches";
            this.itemFill100PercMatches.Size = new System.Drawing.Size(168, 22);
            this.itemFill100PercMatches.Text = "Fill 100% Matches";
            this.itemFill100PercMatches.Click += new System.EventHandler(this.itemFill100PercMatches_Click);
            // 
            // iconImageList
            // 
            this.iconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconImageList.ImageStream")));
            this.iconImageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.iconImageList.Images.SetKeyName(0, "Solution_8308_24.bmp");
            this.iconImageList.Images.SetKeyName(1, "Files_7954_24.bmp");
            this.iconImageList.Images.SetKeyName(2, "Nextrequest_10302_24.bmp");
            this.iconImageList.Images.SetKeyName(3, "Reports-collapsed_12995_24.bmp");
            this.iconImageList.Images.SetKeyName(4, "GenericVSEditor_9905_24.bmp");
            this.iconImageList.Images.SetKeyName(5, "Generatedfile_430_24.bmp");
            this.iconImageList.Images.SetKeyName(6, "CSharpProject_SolutionExplorerNode_24.bmp");
            this.iconImageList.Images.SetKeyName(7, "VBProject_SolutionExplorerNode_24.bmp");
            this.iconImageList.Images.SetKeyName(8, "FSharpProject_SolutionExplorerNode_24.bmp");
            this.iconImageList.Images.SetKeyName(9, "GenericVSProject_9906_16x_24.bmp");
            // 
            // contextFileGroup
            // 
            this.contextFileGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportFileGroup});
            this.contextFileGroup.Name = "contextFileGroup";
            this.contextFileGroup.Size = new System.Drawing.Size(159, 26);
            // 
            // exportFileGroup
            // 
            this.exportFileGroup.Name = "exportFileGroup";
            this.exportFileGroup.Size = new System.Drawing.Size(158, 22);
            this.exportFileGroup.Text = "exportFileGroup";
            this.exportFileGroup.Click += new System.EventHandler(this.exportFileGroup_Click);
            // 
            // contextCulture
            // 
            this.contextCulture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCulture});
            this.contextCulture.Name = "contextCulture";
            this.contextCulture.Size = new System.Drawing.Size(153, 48);
            // 
            // exportCulture
            // 
            this.exportCulture.Name = "exportCulture";
            this.exportCulture.Size = new System.Drawing.Size(152, 22);
            this.exportCulture.Text = "exportCulture";
            this.exportCulture.Click += new System.EventHandler(this.exportCulture_Click);
            // 
            // SolutionTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "SolutionTree";
            this.contextMenuResxFile.ResumeLayout(false);
            this.contextMenuProject.ResumeLayout(false);
            this.contextMenuAnalysis.ResumeLayout(false);
            this.contextMenuAnalysisLang.ResumeLayout(false);
            this.contextFileGroup.ResumeLayout(false);
            this.contextCulture.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuResxFile;
        private System.Windows.Forms.ToolStripMenuItem itemSetCulture;
        private System.Windows.Forms.ContextMenuStrip contextMenuProject;
        private System.Windows.Forms.ToolStripMenuItem exportProject;
        private System.Windows.Forms.ToolStripMenuItem itemImportFromExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalysis;
        private System.Windows.Forms.ToolStripMenuItem itemRefreshAnalysis;
        private System.Windows.Forms.ToolStripComboBox cbCultureInfos;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalysisLang;
        private System.Windows.Forms.ToolStripMenuItem itemFill100PercMatches;
        private System.Windows.Forms.ImageList iconImageList;
        private System.Windows.Forms.ContextMenuStrip contextFileGroup;
        private System.Windows.Forms.ToolStripMenuItem exportFileGroup;
        private System.Windows.Forms.ToolStripMenuItem exportCulturePair;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextCulture;
        private System.Windows.Forms.ToolStripMenuItem exportCulture;
    }
}
