namespace ResourcenManager.Client.Controls
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuResxFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSetCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.cbCultureInfos = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.itemImportFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuAnalysis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRefreshAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuResxFile.SuspendLayout();
            this.contextMenuProject.SuspendLayout();
            this.contextMenuAnalysis.SuspendLayout();
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
            this.contextMenuResxFile.Size = new System.Drawing.Size(180, 48);
            // 
            // itemSetCulture
            // 
            this.itemSetCulture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbCultureInfos});
            this.itemSetCulture.Name = "itemSetCulture";
            this.itemSetCulture.Size = new System.Drawing.Size(179, 22);
            this.itemSetCulture.Text = "toolStripMenuItem1";
            this.itemSetCulture.Click += new System.EventHandler(this.itemSetCulture_Click);
            // 
            // cbCultureInfos
            // 
            this.cbCultureInfos.Name = "cbCultureInfos";
            this.cbCultureInfos.Size = new System.Drawing.Size(250, 21);
            this.cbCultureInfos.Click += new System.EventHandler(this.cbCultureInfos_Click);
            // 
            // contextMenuProject
            // 
            this.contextMenuProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExportToExcel,
            this.itemImportFromExcel});
            this.contextMenuProject.Name = "contextMenuProject";
            this.contextMenuProject.Size = new System.Drawing.Size(176, 48);
            // 
            // itemExportToExcel
            // 
            this.itemExportToExcel.Name = "itemExportToExcel";
            this.itemExportToExcel.Size = new System.Drawing.Size(175, 22);
            this.itemExportToExcel.Text = "itemExportToExcel";
            this.itemExportToExcel.Click += new System.EventHandler(this.itemExportToExcel_Click);
            // 
            // itemImportFromExcel
            // 
            this.itemImportFromExcel.Name = "itemImportFromExcel";
            this.itemImportFromExcel.Size = new System.Drawing.Size(175, 22);
            this.itemImportFromExcel.Text = "itemImportFromExcel";
            this.itemImportFromExcel.Click += new System.EventHandler(this.itemImportFromExcel_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Excel-Document|*.xls";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Excel-Document|*.xls";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // contextMenuAnalysis
            // 
            this.contextMenuAnalysis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRefreshAnalysis});
            this.contextMenuAnalysis.Name = "contextMenuAnalysis";
            this.contextMenuAnalysis.Size = new System.Drawing.Size(180, 48);
            // 
            // itemRefreshAnalysis
            // 
            this.itemRefreshAnalysis.Name = "itemRefreshAnalysis";
            this.itemRefreshAnalysis.Size = new System.Drawing.Size(179, 22);
            this.itemRefreshAnalysis.Text = "toolStripMenuItem1";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuResxFile;
        private System.Windows.Forms.ToolStripMenuItem itemSetCulture;
        private System.Windows.Forms.ContextMenuStrip contextMenuProject;
        private System.Windows.Forms.ToolStripMenuItem itemExportToExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem itemImportFromExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalysis;
        private System.Windows.Forms.ToolStripMenuItem itemRefreshAnalysis;
        private System.Windows.Forms.ToolStripComboBox cbCultureInfos;
    }
}
