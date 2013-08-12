namespace ResourceManager.Client
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
            this.itemCloseSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSaveResources = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.translationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeAllTranslationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSetupDb = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemImportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openExcelDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.solutionTree1 = new ResourceManager.Client.Controls.SolutionTree();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.translationsToolStripMenuItem,
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
            this.itemCloseSolution,
            this.itemSaveResources,
            this.toolStripSeparator1,
            this.itemClose});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // itemOpenSolution
            // 
            this.itemOpenSolution.Name = "itemOpenSolution";
            this.itemOpenSolution.Size = new System.Drawing.Size(180, 22);
            this.itemOpenSolution.Text = "Solution �ffnen";
            this.itemOpenSolution.Click += new System.EventHandler(this.solution�ffnenToolStripMenuItem_Click);
            // 
            // itemCloseSolution
            // 
            this.itemCloseSolution.Enabled = false;
            this.itemCloseSolution.Name = "itemCloseSolution";
            this.itemCloseSolution.Size = new System.Drawing.Size(180, 22);
            this.itemCloseSolution.Text = "toolStripMenuItem1";
            this.itemCloseSolution.Click += new System.EventHandler(this.itemCloseSolution_Click);
            // 
            // itemSaveResources
            // 
            this.itemSaveResources.Name = "itemSaveResources";
            this.itemSaveResources.Size = new System.Drawing.Size(180, 22);
            this.itemSaveResources.Text = "itemSaveResources";
            this.itemSaveResources.Click += new System.EventHandler(this.itemSaveResources_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // itemClose
            // 
            this.itemClose.Name = "itemClose";
            this.itemClose.Size = new System.Drawing.Size(180, 22);
            this.itemClose.Text = "Beenden";
            this.itemClose.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // translationsToolStripMenuItem
            // 
            this.translationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.storeAllTranslationsToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItemSetupDb});
            this.translationsToolStripMenuItem.Name = "translationsToolStripMenuItem";
            this.translationsToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.translationsToolStripMenuItem.Text = "Translations";
            // 
            // storeAllTranslationsToolStripMenuItem
            // 
            this.storeAllTranslationsToolStripMenuItem.Name = "storeAllTranslationsToolStripMenuItem";
            this.storeAllTranslationsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.storeAllTranslationsToolStripMenuItem.Text = "StoreAllTranslations";
            this.storeAllTranslationsToolStripMenuItem.Click += new System.EventHandler(this.storeAllTranslationsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(176, 6);
            // 
            // toolStripMenuItemSetupDb
            // 
            this.toolStripMenuItemSetupDb.Name = "toolStripMenuItemSetupDb";
            this.toolStripMenuItemSetupDb.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemSetupDb.Text = "SetupDatabase";
            this.toolStripMenuItemSetupDb.Click += new System.EventHandler(this.toolStripMenuItemSetupDb_Click);
            // 
            // resourcenToolStripMenuItem
            // 
            this.resourcenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExportAll,
            this.toolStripSeparator2,
            this.itemImportAll});
            this.resourcenToolStripMenuItem.Name = "resourcenToolStripMenuItem";
            this.resourcenToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.resourcenToolStripMenuItem.Text = "Ressourcen";
            // 
            // itemExportAll
            // 
            this.itemExportAll.Name = "itemExportAll";
            this.itemExportAll.Size = new System.Drawing.Size(180, 22);
            this.itemExportAll.Text = "toolStripMenuItem1";
            this.itemExportAll.Click += new System.EventHandler(this.itemExportAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // itemImportAll
            // 
            this.itemImportAll.Enabled = false;
            this.itemImportAll.Name = "itemImportAll";
            this.itemImportAll.Size = new System.Drawing.Size(180, 22);
            this.itemImportAll.Text = "Import";
            this.itemImportAll.Click += new System.EventHandler(this.itemImportAll_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Visual-Studio Solution|*.sln";
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
            // solutionTree1
            // 
            this.solutionTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionTree1.Location = new System.Drawing.Point(0, 24);
            this.solutionTree1.Main = null;
            this.solutionTree1.Name = "solutionTree1";
            this.solutionTree1.Size = new System.Drawing.Size(363, 434);
            this.solutionTree1.Solution = null;
            this.solutionTree1.TabIndex = 1;
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
        private ResourceManager.Client.Controls.SolutionTree solutionTree1;
        private System.Windows.Forms.ToolStripMenuItem itemSaveResources;
        private System.Windows.Forms.ToolStripMenuItem itemExportAll;
        private System.Windows.Forms.ToolStripMenuItem itemImportAll;
        private System.Windows.Forms.OpenFileDialog openExcelDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolBarStatus;
        private System.Windows.Forms.ToolStripProgressBar toolBarProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem translationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeAllTranslationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemCloseSolution;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetupDb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

