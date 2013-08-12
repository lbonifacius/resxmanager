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

        public VSProject Project
        {
            get;
            set;
        }
        private VSSolution solution;
        public VSSolution Solution
        {
            get 
            {
                if (solution == null && Project != null)
                    return Project.Solution;
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
            btnCancel.Text = Properties.Resources.Cancel;
            btnExport.Text = Properties.Resources.Export;

            saveFileDialog.Filter = Properties.Resources.ExcelFileFilter;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(Project != null)
                saveFileDialog.FileName = Project.Name + ".xlsx";
            else
                saveFileDialog.FileName = Solution.Name + ".xlsx";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                XlsxConverter excel = null;
                if(Project != null)
                    excel = new XlsxConverter(Project);
                else
                    excel = new XlsxConverter(Solution);

                excel.ExportComments = cbkExportComments.Checked;
                excel.ExportDiff = cbkExportDiff.Checked;
                excel.Export(saveFileDialog.FileName);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
