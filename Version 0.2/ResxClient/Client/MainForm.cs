using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ResourceManager.Core;
using System.Xml;
using System.Reflection;
using System.IO;
using ResourceManager.Properties;
using System.Threading;
using ResourceManager.Storage;
using System.Globalization;
using System.Linq;

namespace ResourceManager.Client
{
    public partial class MainForm : Form
    {
        private VSSolution vssolution;

        public MainForm()
        {
            InitializeComponent();

            this.Text = Resources.ClientTitle;

            this.dateiToolStripMenuItem.Text = Resources.File;
            this.resourcenToolStripMenuItem.Text = Resources.ResourcesMenu;

            this.itemSaveResources.Text = Resources.SaveResources;
            this.itemClose.Text = Resources.Exit;
            this.itemOpenSolution.Text = Resources.OpenSolution;
            this.itemExportAll.Text = Resources.ExportToExcel;
            this.itemImportAll.Text = Resources.ImportFromExcel;
            this.itemExportDiff.Text = Resources.ExportToExcelDiff;

            this.itemExportAll.Enabled = false;
            this.itemSaveResources.Enabled = false;
            this.storeAllTranslationsToolStripMenuItem.Enabled = false;

            this.openExcelDialog.FileOk += new CancelEventHandler(openExcelDialog_FileOk);

            this.openExcelDialog.Filter = Properties.Resources.ExcelFileFilter;
            this.saveExcelDialog.Filter = Properties.Resources.ExcelFileFilter;
            this.openFileDialog.Filter = Properties.Resources.VSSolutionFileFilter;

            this.storeAllTranslationsToolStripMenuItem.Text = Properties.Resources.StoreAllTranslations;

            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void solutionÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() => openSolution(openFileDialog.FileName));    
            }
        }

        #region Status text
        private System.Threading.Timer toolbarStatusTimer;
        private void setToolbarStatusText(string text)
        {
            this.statusStrip1.Invoke((MethodInvoker)(() => this.toolBarStatus.Text = text));
        }
        private void setToolbarStatusText(string text, int milliseconds)
        {
            this.statusStrip1.Invoke((MethodInvoker)(() => this.toolBarStatus.Text = text));

            TimerCallback tcb = this.resetToolbarStatusText_Elapsed;

            toolbarStatusTimer = new System.Threading.Timer(tcb, null, milliseconds, 10000);  
        }
        void resetToolbarStatusText_Elapsed(object state)
        {
            if (toolbarStatusTimer != null)
            {
                toolbarStatusTimer.Dispose();
                toolbarStatusTimer = null;
            }

            this.resetToolbarStatusText();
        }
        private void resetToolbarStatusText()
        {
            this.statusStrip1.Invoke((MethodInvoker)(() => this.toolBarStatus.Text = ""));
        }
        #endregion

        #region Open, save, export, import
        private void openSolution(string filename)
        {
            setToolbarStatusText(Resources.LoadingSolution);

            vssolution = new VSSolution(filename);

            this.solutionTree1.Solution = vssolution;

            this.menuStrip1.Invoke((MethodInvoker)(() => this.itemExportAll.Enabled = true));
            this.menuStrip1.Invoke((MethodInvoker)(() => this.itemSaveResources.Enabled = true));
            this.menuStrip1.Invoke((MethodInvoker)(() => this.itemExportDiff.Enabled = true));
            this.menuStrip1.Invoke((MethodInvoker)(() => this.itemImportAll.Enabled = true));
            this.menuStrip1.Invoke((MethodInvoker)(() => this.storeAllTranslationsToolStripMenuItem.Enabled = true));

            resetToolbarStatusText();
        }
        private void saveResourceFiles()
        {
            setToolbarStatusText(Resources.SavingResourceFiles);

            foreach (VSProject project in vssolution.Projects.Values)
            {
                foreach (IResourceFileGroup fileGroup in project.ResxGroups.Values)
                {
                    foreach (IResourceFile file in fileGroup.Files.Values)
                    {
                        file.Save();
                    }
                }
            }

            setToolbarStatusText(Resources.SavingResourceFilesCompleted, 4000);
        }
        #endregion

        private void itemSaveResources_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() => saveResourceFiles());           
        }
        

        private void itemExportAll_Click(object sender, EventArgs e)
        {
            saveExcelDialog.FileName = this.solutionTree1.Solution.Name + ".xls";
            DialogResult result = saveExcelDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ExcelConverter excel = new ExcelConverter(this.solutionTree1.Solution);
                excel.Export().Save(saveExcelDialog.FileName);
            }
        }

        private void itemImportAll_Click(object sender, EventArgs e)
        {
            this.openExcelDialog.ShowDialog();
        }
        private void openExcelDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExcelConverter excel = new ExcelConverter(this.solutionTree1.Solution);
            excel.Import(openExcelDialog.FileName);
        }

        private void itemExportDiff_Click(object sender, EventArgs e)
        {
            saveExcelDialog.FileName = this.solutionTree1.Solution.Name + ".xls";
            DialogResult result = saveExcelDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ExcelConverter excel = new ExcelConverter(this.solutionTree1.Solution);
                excel.ExportDiff = true;
                excel.Export().Save(saveExcelDialog.FileName);
            }
        }

        private void storeAllTranslationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() => storeAllTranslations()); 
        }

        private void storeAllTranslations()
        {
            setToolbarStatusText(Resources.StoringTranslations);

            TranslationStorageManager manager = new TranslationStorageManager();
            manager.Store(this.solutionTree1.Solution);

            setToolbarStatusText(Resources.StoringTranslationsCompleted, 4000);
        }        
    }
}