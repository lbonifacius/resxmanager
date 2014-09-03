using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Converter;
using ResourceManager.Core;

namespace ResourceManager.Client
{
    public partial class ExcelExport : Form
    {
        public ExcelExport()
        {
            InitializeComponent();
        }

        private IList<VSProject> selectedProjects = new List<VSProject>();
        public IList<VSProject> SelectedProjects
        {
            get { return selectedProjects; }
        }
        private IList<VSCulture> selectedCultures = new List<VSCulture>();
        public IList<VSCulture> SelectedCultures
        {
            get { return selectedCultures; }
        }
        private IList<IResourceFileGroup> selectedFileGroups = new List<IResourceFileGroup>();
        public IList<IResourceFileGroup> SelectedFileGroups
        {
            get { return selectedFileGroups; }
        }

        private VSSolution solution;
        public VSSolution Solution
        {
            get 
            {
                if (solution == null && SelectedProjects != null && SelectedProjects.Count() > 0)
                    return SelectedProjects[0].Solution;
                else
                    return solution;
            }
            set
            {
                solution = value;
            }
        }

        private void ExcelExport_Load(object sender, EventArgs e)
        {
            cbkExportComments.Text = Properties.Resources.ExportComments;
            cbkExportDiff.Text = Properties.Resources.ExportDiff;
	        cbkIncludeProjectsWithoutTranslations.Text = Properties.Resources.IncludeProjectsWithoutTranslations;
            cbkAutoAdjustLayout.Text = Properties.Resources.AutoAdjustLayout;
            cbkIgnoreInternalResources.Text = Resources.IgnoreInternalResources;
            btnCancel.Text = Properties.Resources.Cancel;
            btnExport.Text = Properties.Resources.Export;
            lbCultures.Text = Properties.Resources.Cultures;
            lbProjects.Text = Properties.Resources.Projects;

            saveFileDialog.Filter = Properties.Resources.ExcelFileFilter;

            if (SelectedProjects.Count == 0)
                projects.LoadProjects(Solution, Solution.Projects.Values);
            else
                projects.LoadProjects(Solution, SelectedProjects);

            projects.Enabled = SelectedFileGroups.Count == 0;

            if (SelectedCultures.Count == 0)
                cultures.LoadCultures(Solution, Solution.Cultures.Values);
            else
                cultures.LoadCultures(Solution, SelectedCultures);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            this.btnCancel.Enabled = false;

            selectedProjects = projects.GetSelectedProjects();
            selectedCultures = cultures.GetSelectedCultures();

            if (SelectedProjects != null)
            {
                if(SelectedProjects.Count() == Solution.Projects.Count())
                    saveFileDialog.FileName = Solution.Name;
                else if (SelectedProjects.Count() > 1)
                    saveFileDialog.FileName = SelectedProjects[0].Name + "-more";
                else
                    saveFileDialog.FileName = SelectedProjects[0].Name;
            }
            else
                saveFileDialog.FileName = Solution.Name;

            if (SelectedCultures.Count() == 1)
            {
                if (SelectedCultures.First().Culture.Name != "")
                    saveFileDialog.FileName += "-" + SelectedCultures.First().Culture.Name;
            }
            else if (SelectedCultures.Count() == 2)
            {
                saveFileDialog.FileName += "-" + SelectedCultures.First().Culture.Name
                    + "," + SelectedCultures.ElementAt(1).Culture.Name;
            }
            saveFileDialog.FileName += ".xlsx";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    XlsxConverter excel = null;
                    if (SelectedFileGroups.Count != 0)
                        excel = new XlsxConverter(SelectedFileGroups, Solution);
                    else if (SelectedProjects.Count != 0)
                        excel = new XlsxConverter(SelectedProjects);
                    else 
                        excel = new XlsxConverter(Solution);

                    excel.ExportComments = cbkExportComments.Checked;
                    excel.ExportDiff = cbkExportDiff.Checked;
	                excel.IncludeProjectsWithoutTranslations = cbkIncludeProjectsWithoutTranslations.Checked;
                    excel.AutoAdjustLayout = cbkAutoAdjustLayout.Checked;
                    excel.IgnoreInternalResources = cbkIgnoreInternalResources.Checked;
                    excel.Cultures = cultures.GetSelectedCultures();

                    excel.Export(saveFileDialog.FileName);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    this.btnExport.Enabled = true;
                    this.btnCancel.Enabled = true;
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
