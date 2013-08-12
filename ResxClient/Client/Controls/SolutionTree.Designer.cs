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
            this.itemExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.itemImportFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuAnalysis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRefreshAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAnalysisLang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemFill100PercMatches = new System.Windows.Forms.ToolStripMenuItem();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuResxFile.SuspendLayout();
            this.contextMenuProject.SuspendLayout();
            this.contextMenuAnalysis.SuspendLayout();
            this.contextMenuAnalysisLang.SuspendLayout();
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
            this.itemExportToExcel,
            this.itemImportFromExcel});
            this.contextMenuProject.Name = "contextMenuProject";
            this.contextMenuProject.Size = new System.Drawing.Size(189, 48);
            // 
            // itemExportToExcel
            // 
            this.itemExportToExcel.Name = "itemExportToExcel";
            this.itemExportToExcel.Size = new System.Drawing.Size(188, 22);
            this.itemExportToExcel.Text = "itemExportToExcel";
            this.itemExportToExcel.Click += new System.EventHandler(this.itemExportToExcel_Click);
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
            this.itemRefreshAnalysis.Text = "toolStripMenuItem1";
            // 
            // contextMenuAnalysisLang
            // 
            this.contextMenuAnalysisLang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemFill100PercMatches});
            this.contextMenuAnalysisLang.Name = "contextMenuAnalysisLang";
            this.contextMenuAnalysisLang.Size = new System.Drawing.Size(169, 26);
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
            this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconImageList.Images.SetKeyName(0, "Solution.ico");
            this.iconImageList.Images.SetKeyName(1, "Project.ico");
            this.iconImageList.Images.SetKeyName(2, "ResxFileGroup.ico");
            this.iconImageList.Images.SetKeyName(3, "ResxFile.ico");
            this.iconImageList.Images.SetKeyName(4, "Book_StackOfReportsHS.ico");
            this.iconImageList.Images.SetKeyName(5, "book_reportHS.ico");
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuResxFile;
        private System.Windows.Forms.ToolStripMenuItem itemSetCulture;
        private System.Windows.Forms.ContextMenuStrip contextMenuProject;
        private System.Windows.Forms.ToolStripMenuItem itemExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem itemImportFromExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalysis;
        private System.Windows.Forms.ToolStripMenuItem itemRefreshAnalysis;
        private System.Windows.Forms.ToolStripComboBox cbCultureInfos;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalysisLang;
        private System.Windows.Forms.ToolStripMenuItem itemFill100PercMatches;
        private System.Windows.Forms.ImageList iconImageList;
    }
}
