namespace ResourcenManager.Client
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOpenSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSaveResources = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExportDiff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemImportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openExcelDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveExcelDialog = new System.Windows.Forms.SaveFileDialog();
            this.solutionTree1 = new ResourcenManager.Client.Controls.SolutionTree();
            this.toolBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.resourcenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(363, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemOpenSolution,
            this.itemSaveResources,
            this.toolStripSeparator1,
            this.itemClose});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // itemOpenSolution
            // 
            this.itemOpenSolution.Name = "itemOpenSolution";
            this.itemOpenSolution.Size = new System.Drawing.Size(179, 22);
            this.itemOpenSolution.Text = "Solution öffnen";
            this.itemOpenSolution.Click += new System.EventHandler(this.solutionÖffnenToolStripMenuItem_Click);
            // 
            // itemSaveResources
            // 
            this.itemSaveResources.Name = "itemSaveResources";
            this.itemSaveResources.Size = new System.Drawing.Size(179, 22);
            this.itemSaveResources.Text = "itemSaveResources";
            this.itemSaveResources.Click += new System.EventHandler(this.itemSaveResources_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // itemClose
            // 
            this.itemClose.Name = "itemClose";
            this.itemClose.Size = new System.Drawing.Size(179, 22);
            this.itemClose.Text = "Beenden";
            this.itemClose.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // resourcenToolStripMenuItem
            // 
            this.resourcenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExportAll,
            this.itemExportDiff,
            this.toolStripSeparator2,
            this.itemImportAll});
            this.resourcenToolStripMenuItem.Name = "resourcenToolStripMenuItem";
            this.resourcenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.resourcenToolStripMenuItem.Text = "Ressourcen";
            // 
            // itemExportAll
            // 
            this.itemExportAll.Name = "itemExportAll";
            this.itemExportAll.Size = new System.Drawing.Size(179, 22);
            this.itemExportAll.Text = "toolStripMenuItem1";
            this.itemExportAll.Click += new System.EventHandler(this.itemExportAll_Click);
            // 
            // itemExportDiff
            // 
            this.itemExportDiff.Enabled = false;
            this.itemExportDiff.Name = "itemExportDiff";
            this.itemExportDiff.Size = new System.Drawing.Size(179, 22);
            this.itemExportDiff.Text = "Export Diff";
            this.itemExportDiff.Click += new System.EventHandler(this.itemExportDiff_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // itemImportAll
            // 
            this.itemImportAll.Enabled = false;
            this.itemImportAll.Name = "itemImportAll";
            this.itemImportAll.Size = new System.Drawing.Size(179, 22);
            this.itemImportAll.Text = "Import";
            this.itemImportAll.Click += new System.EventHandler(this.itemImportAll_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Visual-Studio Solution|*.sln";
            // 
            // openExcelDialog
            // 
            this.openExcelDialog.FileName = "openFileDialog1";
            // 
            // saveExcelDialog
            // 
            this.saveExcelDialog.Filter = "Excel-Document|*.xls";
            // 
            // solutionTree1
            // 
            this.solutionTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionTree1.Location = new System.Drawing.Point(0, 24);
            this.solutionTree1.Name = "solutionTree1";
            this.solutionTree1.Size = new System.Drawing.Size(363, 434);
            this.solutionTree1.Solution = null;
            this.solutionTree1.TabIndex = 1;
            // 
            // toolBarStatus
            // 
            this.toolBarStatus.Name = "toolBarStatus";
            this.toolBarStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolBarProgress
            // 
            this.toolBarProgress.Name = "toolBarProgress";
            this.toolBarProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(363, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 458);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.solutionTree1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Resourcen Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemClose;
        private System.Windows.Forms.ToolStripMenuItem resourcenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem itemOpenSolution;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ResourcenManager.Client.Controls.SolutionTree solutionTree1;
        private System.Windows.Forms.ToolStripMenuItem itemSaveResources;
        private System.Windows.Forms.ToolStripMenuItem itemExportAll;
        private System.Windows.Forms.ToolStripMenuItem itemImportAll;
        private System.Windows.Forms.OpenFileDialog openExcelDialog;
        private System.Windows.Forms.SaveFileDialog saveExcelDialog;
        private System.Windows.Forms.ToolStripMenuItem itemExportDiff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolBarStatus;
        private System.Windows.Forms.ToolStripProgressBar toolBarProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

