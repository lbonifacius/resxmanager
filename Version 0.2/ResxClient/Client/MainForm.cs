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
using ResourceManager.Converter;
using System.Threading.Tasks;

namespace ResourceManager.Client
{
    public partial class MainForm : Form
    {        
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainForm));

        public MainForm()
        {
            InitializeComponent();

            this.Text = Resources.ClientTitle;

            this.dateiToolStripMenuItem.Text = Resources.File;
            this.translationsToolStripMenuItem.Text = Resources.Translations;
            this.toolStripMenuItemDocu.Text = Resources.Documentation;

            this.itemSaveResources.Text = Resources.SaveResources;
            this.itemClose.Text = Resources.Exit;
            this.itemOpenSolution.Text = Resources.OpenSolution;
            this.itemCloseSolution.Text = Resources.CloseSolution;
            this.toolStripMenuItemExport.Text = Resources.ExportToExcel;
            this.toolStripMenuItemImport.Text = Resources.ImportFromExcel;
            this.toolStripMenuItemSetupDb.Text = Resources.SetupDatabase;

            this.toolStripMenuItemTranslateAll.Text = Resources.TranslateAuto;
            this.toolStripMenuItemTranslateAll.ToolTipText = Resources.TranslateToolTip;
            this.toolFillTranslations.Text = Resources.Translate;
            this.toolFillTranslations.ToolTipText = Resources.TranslateToolTip;
            this.toolExport.Text = Resources.Export;
            this.toolExport.ToolTipText = Resources.ExportToExcel;
            this.toolImport.Text = Resources.Import;
            this.toolImport.ToolTipText = Resources.ImportFromExcel;
            this.toolOpen.Text = Resources.Open;
            this.toolOpen.ToolTipText = Resources.OpenSolution;
            this.toolSaveAll.ToolTipText = Resources.SaveResources;

            this.itemSaveResources.Enabled = false;
            this.storeAllTranslationsToolStripMenuItem.Enabled = false;

            this.openExcelDialog.FileOk += new CancelEventHandler(openExcelDialog_FileOk);

            this.openExcelDialog.Filter = Properties.Resources.ExcelImportFileFilter;
            this.openFileDialog.Filter = Properties.Resources.VSSolutionFileFilter;

            this.storeAllTranslationsToolStripMenuItem.Text = Properties.Resources.StoreAllTranslations;

            this.solutionTree1.Main = this;

            this.FormClosing += MainForm_FormClosing;
        }
        public VSSolution CurrentSolution
        {
            get;
            private set;
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            var result = testSolutionHasChanged();
            e.Cancel = result == System.Windows.Forms.DialogResult.Cancel;
        }
       
        internal Task startNewTask(Action action)
        {
            var task = System.Threading.Tasks.Task.Factory.StartNew(action)
            .ContinueWith((t) =>
            {
                if (log.IsErrorEnabled)
                    log.Error("Error while processing task.", t.Exception);

                ExceptionHandling.ShowErrorDialog(t.Exception);
            },
            System.Threading.Tasks.TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }

        #region Status text
        private System.Threading.Timer toolbarStatusTimer;
        public void setToolbarStatusText(string text)
        {
            this.statusStrip1.Invoke((MethodInvoker)(() => this.toolBarStatus.Text = text));
        }
        public void setToolbarStatusText(string text, int milliseconds)
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
        private void openSolution()
        {
            var scresult = testSolutionHasChanged();
            if (scresult != System.Windows.Forms.DialogResult.Cancel)
            {
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    startNewTask(() => openSolution(openFileDialog.FileName));
                }
            }
        }
        private void openSolution(string filename)
        {
            try
            {
                setToolbarStatusText(Resources.LoadingSolution);

                CurrentSolution = new VSSolution(filename);

                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemExport.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.itemSaveResources.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemImport.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.itemOpenSolution.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.storeAllTranslationsToolStripMenuItem.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.itemCloseSolution.Enabled = true));

                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolSaveAll.Enabled = true));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolImport.Enabled = true));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolExport.Enabled = true));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolFillTranslations.Enabled = true));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemTranslateAll.Enabled = true));

                solutionTree1.LoadTree();
            }
            catch
            {
                throw;
            }
            finally
            {                
                resetToolbarStatusText();
            }

            if (CurrentSolution.Projects.Count == 0)
            {
                MessageBox.Show(String.Format(Properties.Resources.WarningNoProjects, filename), Properties.Resources.WarningOpenSolutionTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CurrentSolution.Cultures.Count == 0)
            {
                MessageBox.Show(String.Format(Properties.Resources.WarningNoCultures, filename), Properties.Resources.WarningOpenSolutionTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void closeSolution()
        {
            var result = testSolutionHasChanged();

            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                this.CurrentSolution = null;

                solutionTree1.LoadTree();

                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemExport.Enabled = false));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.itemSaveResources.Enabled = false));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemImport.Enabled = false));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.storeAllTranslationsToolStripMenuItem.Enabled = false));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.itemCloseSolution.Enabled = false));

                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolSaveAll.Enabled = false));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolImport.Enabled = false));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolExport.Enabled = false));
                this.toolStrip1.Invoke((MethodInvoker)(() => this.toolFillTranslations.Enabled = false));
                this.menuStrip1.Invoke((MethodInvoker)(() => this.toolStripMenuItemTranslateAll.Enabled = false));
            }
        }
        private void saveResourceFiles()
        {
            bool saved = false;
            try
            {
                setToolbarStatusText(Resources.SavingResourceFiles);
                saved = CurrentSolution.Save();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (saved)
                    setToolbarStatusText(Resources.SavingResourceFilesCompleted, 4000);
                else
                    resetToolbarStatusText();
            }
        }
        private void exportAll()
        {
            var excel = new ExcelExport();
            excel.Solution = CurrentSolution;
            excel.ShowDialog();
        }
        private void importAll()
        {
            this.openExcelDialog.ShowDialog();
        }
        private void openExcelDialog_FileOk(object sender, CancelEventArgs e)
        {
            setToolbarStatusText(Resources.ImportingResources);

            IConverter excel = ConverterFactory.OpenConverter(openExcelDialog, CurrentSolution);
            int count = excel.Import(openExcelDialog.FileName);

            solutionTree1.RefreshAnalysis();

            setToolbarStatusText(String.Format(Resources.ImportingResourcesCompleted, count), 4000);
        }
        protected DialogResult testSolutionHasChanged()
        {
            if (CurrentSolution != null && CurrentSolution.HasChanged)
            {
                var result = MessageBox.Show(Properties.Resources.SaveChangesQuestion, Properties.Resources.SaveChangesTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    saveResourceFiles();
                }

                return result;
            }

            return System.Windows.Forms.DialogResult.Ignore;
        }
        #endregion

        #region Menu & toolbar events
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void solutionÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSolution();
        }
        private void itemSaveResources_Click(object sender, EventArgs e)
        {
            startNewTask(() => saveResourceFiles());        
        }
        private void itemExportAll_Click(object sender, EventArgs e)
        {
            exportAll();
        }
        private void itemImportAll_Click(object sender, EventArgs e)
        {
            importAll();
        }        
        private void storeAllTranslationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startNewTask(() => storeAllTranslations()); 
        }
        private void itemCloseSolution_Click(object sender, EventArgs e)
        {
            closeSolution();
        }
        private void toolStripMenuItemSetupDb_Click(object sender, EventArgs e)
        {
            openSetupDatabase();
        }
        private void toolOpen_Click(object sender, EventArgs e)
        {
            openSolution();
        }
        private void toolSaveAll_Click(object sender, EventArgs e)
        {
            startNewTask(() => saveResourceFiles());  
        }
        private void toolExport_Click(object sender, EventArgs e)
        {
            exportAll();
        }
        private void toolImport_Click(object sender, EventArgs e)
        {
            importAll();
        }
        private void toolFillTranslations_Click(object sender, EventArgs e)
        {
            startNewTask(() => fillTranslations());
        }
        private void toolStripMenuItemImport_Click(object sender, EventArgs e)
        {
            importAll();
        }
        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            exportAll();
        }
        private void toolStripMenuItemDocu_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://resxmanager.codeplex.com/documentation");
        }
        private void toolStripMenuItemTranslateAll_Click(object sender, EventArgs e)
        {
            startNewTask(() => fillTranslations());

            solutionTree1.RefreshAnalysis();
        }

        #endregion

        #region Translation
        private void openSetupDatabase()
        {
            var dialog = new SetupDatabase();
            dialog.Show();
        }
        private void storeAllTranslations()
        {
            if (CheckDatabase())
            {
                try
                {
                    setToolbarStatusText(Resources.StoringTranslations);

                    TranslationStorageManager manager = new TranslationStorageManager();
                    if (!manager.DatabaseExists())
                    {
                        manager.CreateDatabase();
                    }
                    manager.Store(CurrentSolution);

                    setToolbarStatusText(Resources.StoringTranslationsCompleted, 4000);
                }
                catch
                {
                    resetToolbarStatusText();

                    throw;
                }
                finally
                {
                }
            }
        }
        internal void fillTranslations(ResourceManager.Client.Controls.CultureAnalysisResultTreeNode node)
        {
            if (CheckDatabase())
            {
                int found = fillTranslations(node.SourceCulture, node.TargetCulture);

                if (found > 0)
                    this.solutionTree1.RefreshAnalysis(node);
            }
        }
        internal int fillTranslations(VSCulture sourceCulture, VSCulture targetCulture)
        {
            int found = 0;

            try
            {
                setToolbarStatusText(Properties.Resources.SearchingTranslations);

                List<ResourceDataBase> notexisting = sourceCulture.GetItemsNotExistingInCulture(targetCulture);

                var trans = new TranslationStorageManager();
                int process = 1;
                foreach (var data in notexisting)
                {
                    setToolbarStatusText(String.Format(Properties.Resources.SerachingTranslationsProcess, process, notexisting.Count()));

                    var result = trans.Search(data, targetCulture.Culture);

                    if (result.Count() > 0)
                    {
                        found++;
                        data.ResxFile.FileGroup.SetResourceData(data.Name, result.First().Text, targetCulture.Culture);
                    }
                    process++;
                }

                setToolbarStatusText(String.Format(Properties.Resources.SearchingTranslationsResult, found), 4000);
            }
            catch
            {
                resetToolbarStatusText();

                throw;
            }
            finally
            {
            }

            return found;            
        }
        internal void fillTranslations()
        {
            if (CheckDatabase())
            {
                try
                {
                    setToolbarStatusText(Properties.Resources.SearchingTranslations);

                    int process = 1;
                    int found = 0;
                    int max = CurrentSolution.Cultures.Count * (CurrentSolution.Cultures.Count - 1);
                    var trans = new TranslationStorageManager();

                    foreach (VSCulture sourceCulture in CurrentSolution.Cultures.Values)
                    {
                        foreach (VSCulture targetCulture in CurrentSolution.Cultures.Values.Except(new VSCulture[] { sourceCulture }))
                        {
                            setToolbarStatusText(String.Format(Properties.Resources.SerachingTranslationsProcess, process, max));

                            List<ResourceDataBase> notexisting = sourceCulture.GetItemsNotExistingInCulture(targetCulture);
                            foreach (var data in notexisting)
                            {
                                var result = trans.Search(data, targetCulture.Culture);

                                if (result.Count() > 0)
                                {
                                    found++;
                                    data.ResxFile.FileGroup.SetResourceData(data.Name, result.First().Text, targetCulture.Culture);
                                }

                            }
                            process++;
                        }
                    }

                    if (found > 0)
                        this.solutionTree1.RefreshAnalysis();

                    setToolbarStatusText(String.Format(Properties.Resources.SearchingTranslationsResult, found), 4000);
                }
                catch
                {
                    resetToolbarStatusText();

                    throw;
                }
                finally
                {
                }
            }
        }

        internal bool CheckDatabase()
        {
            if (TranslationStorageManager.CheckDatabase())
                return true;
            else
            {
                MessageBox.Show(Properties.Resources.DataBaseNotConfigured, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Invoke((MethodInvoker)(() => openSetupDatabase()));
                
                return false;            
            }
        }
        #endregion
    }
}