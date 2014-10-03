namespace ResourceManager.Client
{
    partial class ExcelExport
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
            this.cbkExportDiff = new System.Windows.Forms.CheckBox();
            this.cbkExportComments = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cbkIncludeProjectsWithoutTranslations = new System.Windows.Forms.CheckBox();
            this.cbkAutoAdjustLayout = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbCultures = new System.Windows.Forms.Label();
            this.lbProjects = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbkIgnoreInternalResources = new System.Windows.Forms.CheckBox();
            this.projects = new ResourceManager.Client.Controls.ProjectsListView();
            this.cultures = new ResourceManager.Client.Controls.CulturesListBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbkExportDiff
            // 
            this.cbkExportDiff.AutoSize = true;
            this.cbkExportDiff.Location = new System.Drawing.Point(16, 12);
            this.cbkExportDiff.Name = "cbkExportDiff";
            this.cbkExportDiff.Size = new System.Drawing.Size(75, 17);
            this.cbkExportDiff.TabIndex = 0;
            this.cbkExportDiff.Text = "Export Diff";
            this.cbkExportDiff.UseVisualStyleBackColor = true;
            // 
            // cbkExportComments
            // 
            this.cbkExportComments.AutoSize = true;
            this.cbkExportComments.Location = new System.Drawing.Point(16, 36);
            this.cbkExportComments.Name = "cbkExportComments";
            this.cbkExportComments.Size = new System.Drawing.Size(108, 17);
            this.cbkExportComments.TabIndex = 1;
            this.cbkExportComments.Text = "Export Comments";
            this.cbkExportComments.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(104, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(23, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbkIncludeProjectsWithoutTranslations
            // 
            this.cbkIncludeProjectsWithoutTranslations.AutoSize = true;
            this.cbkIncludeProjectsWithoutTranslations.Location = new System.Drawing.Point(242, 12);
            this.cbkIncludeProjectsWithoutTranslations.Name = "cbkIncludeProjectsWithoutTranslations";
            this.cbkIncludeProjectsWithoutTranslations.Size = new System.Drawing.Size(194, 17);
            this.cbkIncludeProjectsWithoutTranslations.TabIndex = 4;
            this.cbkIncludeProjectsWithoutTranslations.Text = "Include projects without translations";
            this.cbkIncludeProjectsWithoutTranslations.UseVisualStyleBackColor = true;
            // 
            // cbkAutoAdjustLayout
            // 
            this.cbkAutoAdjustLayout.AutoSize = true;
            this.cbkAutoAdjustLayout.Checked = true;
            this.cbkAutoAdjustLayout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkAutoAdjustLayout.Location = new System.Drawing.Point(16, 59);
            this.cbkAutoAdjustLayout.Name = "cbkAutoAdjustLayout";
            this.cbkAutoAdjustLayout.Size = new System.Drawing.Size(110, 17);
            this.cbkAutoAdjustLayout.TabIndex = 5;
            this.cbkAutoAdjustLayout.Text = "Auto adjust layout";
            this.cbkAutoAdjustLayout.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 38);
            this.panel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(281, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(191, 38);
            this.panel3.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.projects, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbCultures, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbProjects, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cultures, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(472, 345);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // lbCultures
            // 
            this.lbCultures.AutoSize = true;
            this.lbCultures.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCultures.Location = new System.Drawing.Point(239, 7);
            this.lbCultures.Name = "lbCultures";
            this.lbCultures.Size = new System.Drawing.Size(41, 13);
            this.lbCultures.TabIndex = 8;
            this.lbCultures.Text = "label2";
            // 
            // lbProjects
            // 
            this.lbProjects.AutoSize = true;
            this.lbProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProjects.Location = new System.Drawing.Point(3, 7);
            this.lbProjects.Name = "lbProjects";
            this.lbProjects.Size = new System.Drawing.Size(41, 13);
            this.lbProjects.TabIndex = 7;
            this.lbProjects.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbkIgnoreInternalResources);
            this.panel2.Controls.Add(this.cbkExportDiff);
            this.panel2.Controls.Add(this.cbkExportComments);
            this.panel2.Controls.Add(this.cbkIncludeProjectsWithoutTranslations);
            this.panel2.Controls.Add(this.cbkAutoAdjustLayout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 105);
            this.panel2.TabIndex = 12;
            // 
            // cbkIgnoreInternalResources
            // 
            this.cbkIgnoreInternalResources.AutoSize = true;
            this.cbkIgnoreInternalResources.Location = new System.Drawing.Point(16, 82);
            this.cbkIgnoreInternalResources.Name = "cbkIgnoreInternalResources";
            this.cbkIgnoreInternalResources.Size = new System.Drawing.Size(223, 17);
            this.cbkIgnoreInternalResources.TabIndex = 6;
            this.cbkIgnoreInternalResources.Text = "Ignore WinForms internal resources (\">>\")";
            this.cbkIgnoreInternalResources.UseVisualStyleBackColor = true;
            // 
            // projects
            // 
            this.projects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projects.Location = new System.Drawing.Point(3, 30);
            this.projects.Name = "projects";
            this.projects.ScrollAlwaysVisible = true;
            this.projects.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.projects.Size = new System.Drawing.Size(230, 312);
            this.projects.TabIndex = 6;
            // 
            // cultures
            // 
            this.cultures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cultures.FormattingEnabled = true;
            this.cultures.Location = new System.Drawing.Point(239, 30);
            this.cultures.Name = "cultures";
            this.cultures.ScrollAlwaysVisible = true;
            this.cultures.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.cultures.Size = new System.Drawing.Size(230, 312);
            this.cultures.TabIndex = 9;
            // 
            // ExcelExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 488);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExcelExport";
            this.Text = "ExcelExport";
            this.Load += new System.EventHandler(this.ExcelExport_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbkExportDiff;
        private System.Windows.Forms.CheckBox cbkExportComments;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
		  private System.Windows.Forms.CheckBox cbkIncludeProjectsWithoutTranslations;
          private System.Windows.Forms.CheckBox cbkAutoAdjustLayout;
          private System.Windows.Forms.Panel panel1;
          private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
          private System.Windows.Forms.Label lbCultures;
          private System.Windows.Forms.Label lbProjects;
          private Controls.ProjectsListView projects;
          private System.Windows.Forms.Panel panel2;
          private System.Windows.Forms.Panel panel3;
          private Controls.CulturesListBox cultures;
          private System.Windows.Forms.CheckBox cbkIgnoreInternalResources;
    }
}