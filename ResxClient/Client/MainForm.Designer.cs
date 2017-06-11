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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOpenSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCloseSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSaveResources = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.itemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.translationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeAllTranslationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSetupDb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDocu = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openExcelDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExport = new System.Windows.Forms.ToolStripButton();
            this.toolImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFillTranslations = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItemTranslateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionTree1 = new ResourceManager.Client.Controls.SolutionTree();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.translationsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(481, 24);
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
            this.toolStripMenuItemImport,
            this.toolStripMenuItemExport,
            this.toolStripSeparator6,
            this.itemClose});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // itemOpenSolution
            // 
            this.itemOpenSolution.Name = "itemOpenSolution";
            this.itemOpenSolution.Size = new System.Drawing.Size(156, 22);
            this.itemOpenSolution.Text = "Open solution";
            this.itemOpenSolution.Click += new System.EventHandler(this.solutionOpenToolStripMenuItem_Click);
            // 
            // itemCloseSolution
            // 
            this.itemCloseSolution.Enabled = false;
            this.itemCloseSolution.Name = "itemCloseSolution";
            this.itemCloseSolution.Size = new System.Drawing.Size(156, 22);
            this.itemCloseSolution.Text = "Close";
            this.itemCloseSolution.Click += new System.EventHandler(this.itemCloseSolution_Click);
            // 
            // itemSaveResources
            // 
            this.itemSaveResources.Name = "itemSaveResources";
            this.itemSaveResources.Size = new System.Drawing.Size(156, 22);
            this.itemSaveResources.Text = "Save";
            this.itemSaveResources.Click += new System.EventHandler(this.itemSaveResources_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // toolStripMenuItemImport
            // 
            this.toolStripMenuItemImport.Enabled = false;
            this.toolStripMenuItemImport.Name = "toolStripMenuItemImport";
            this.toolStripMenuItemImport.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemImport.Text = "Import";
            this.toolStripMenuItemImport.Click += new System.EventHandler(this.toolStripMenuItemImport_Click);
            // 
            // toolStripMenuItemExport
            // 
            this.toolStripMenuItemExport.Enabled = false;
            this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            this.toolStripMenuItemExport.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemExport.Text = "Export";
            this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(153, 6);
            // 
            // itemClose
            // 
            this.itemClose.Name = "itemClose";
            this.itemClose.Size = new System.Drawing.Size(156, 22);
            this.itemClose.Text = "Beenden";
            this.itemClose.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // translationsToolStripMenuItem
            // 
            this.translationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.storeAllTranslationsToolStripMenuItem,
            this.toolStripMenuItemTranslateAll,
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
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDocu});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItemHelp.Text = "?";
            // 
            // toolStripMenuItemDocu
            // 
            this.toolStripMenuItemDocu.Name = "toolStripMenuItemDocu";
            this.toolStripMenuItemDocu.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItemDocu.Text = "Documentation";
            this.toolStripMenuItemDocu.Click += new System.EventHandler(this.toolStripMenuItemDocu_Click);
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
            this.statusStrip1.Size = new System.Drawing.Size(481, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolSaveAll,
            this.toolStripSeparator4,
            this.toolExport,
            this.toolImport,
            this.toolStripSeparator5,
            this.toolFillTranslations});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(481, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOpen
            // 
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(56, 22);
            this.toolOpen.Text = "Open";
            this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolSaveAll
            // 
            this.toolSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSaveAll.Enabled = false;
            this.toolSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveAll.Image")));
            this.toolSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveAll.Name = "toolSaveAll";
            this.toolSaveAll.Size = new System.Drawing.Size(23, 22);
            this.toolSaveAll.Text = "toolStripButton1";
            this.toolSaveAll.Click += new System.EventHandler(this.toolSaveAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolExport
            // 
            this.toolExport.Enabled = false;
            this.toolExport.Image = ((System.Drawing.Image)(resources.GetObject("toolExport.Image")));
            this.toolExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExport.Name = "toolExport";
            this.toolExport.Size = new System.Drawing.Size(60, 22);
            this.toolExport.Text = "Export";
            this.toolExport.Click += new System.EventHandler(this.toolExport_Click);
            // 
            // toolImport
            // 
            this.toolImport.Enabled = false;
            this.toolImport.Image = ((System.Drawing.Image)(resources.GetObject("toolImport.Image")));
            this.toolImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolImport.Name = "toolImport";
            this.toolImport.Size = new System.Drawing.Size(63, 22);
            this.toolImport.Text = "Import";
            this.toolImport.Click += new System.EventHandler(this.toolImport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolFillTranslations
            // 
            this.toolFillTranslations.Enabled = false;
            this.toolFillTranslations.Image = ((System.Drawing.Image)(resources.GetObject("toolFillTranslations.Image")));
            this.toolFillTranslations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFillTranslations.Name = "toolFillTranslations";
            this.toolFillTranslations.Size = new System.Drawing.Size(75, 22);
            this.toolFillTranslations.Text = "Translate";
            this.toolFillTranslations.Click += new System.EventHandler(this.toolFillTranslations_Click);
            // 
            // toolStripMenuItemTranslateAll
            // 
            this.toolStripMenuItemTranslateAll.Enabled = false;
            this.toolStripMenuItemTranslateAll.Name = "toolStripMenuItemTranslateAll";
            this.toolStripMenuItemTranslateAll.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemTranslateAll.Text = "Translate";
            this.toolStripMenuItemTranslateAll.Click += new System.EventHandler(this.toolStripMenuItemTranslateAll_Click);
            // 
            // solutionTree1
            // 
            this.solutionTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionTree1.Location = new System.Drawing.Point(0, 49);
            this.solutionTree1.Main = null;
            this.solutionTree1.Name = "solutionTree1";
            this.solutionTree1.Size = new System.Drawing.Size(481, 387);
            this.solutionTree1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 458);
            this.Controls.Add(this.solutionTree1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Resourcen Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem itemOpenSolution;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ResourceManager.Client.Controls.SolutionTree solutionTree1;
        private System.Windows.Forms.ToolStripMenuItem itemSaveResources;
        private System.Windows.Forms.OpenFileDialog openExcelDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolBarStatus;
        private System.Windows.Forms.ToolStripProgressBar toolBarProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem translationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeAllTranslationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemCloseSolution;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetupDb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripButton toolSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolExport;
        private System.Windows.Forms.ToolStripButton toolImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolFillTranslations;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDocu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTranslateAll;
    }
}

