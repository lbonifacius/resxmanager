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
			this.SuspendLayout();
			// 
			// cbkExportDiff
			// 
			this.cbkExportDiff.AutoSize = true;
			this.cbkExportDiff.Location = new System.Drawing.Point(13, 13);
			this.cbkExportDiff.Name = "cbkExportDiff";
			this.cbkExportDiff.Size = new System.Drawing.Size(80, 17);
			this.cbkExportDiff.TabIndex = 0;
			this.cbkExportDiff.Text = "checkBox1";
			this.cbkExportDiff.UseVisualStyleBackColor = true;
			// 
			// cbkExportComments
			// 
			this.cbkExportComments.AutoSize = true;
			this.cbkExportComments.Location = new System.Drawing.Point(13, 37);
			this.cbkExportComments.Name = "cbkExportComments";
			this.cbkExportComments.Size = new System.Drawing.Size(80, 17);
			this.cbkExportComments.TabIndex = 1;
			this.cbkExportComments.Text = "checkBox2";
			this.cbkExportComments.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(197, 227);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(116, 227);
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
			this.cbkIncludeProjectsWithoutTranslations.Location = new System.Drawing.Point(13, 61);
			this.cbkIncludeProjectsWithoutTranslations.Name = "cbkIncludeProjectsWithoutTranslations";
			this.cbkIncludeProjectsWithoutTranslations.Size = new System.Drawing.Size(80, 17);
			this.cbkIncludeProjectsWithoutTranslations.TabIndex = 4;
			this.cbkIncludeProjectsWithoutTranslations.Text = "checkBox3";
			this.cbkIncludeProjectsWithoutTranslations.UseVisualStyleBackColor = true;
			// 
			// ExcelExport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.cbkIncludeProjectsWithoutTranslations);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cbkExportComments);
			this.Controls.Add(this.cbkExportDiff);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExcelExport";
			this.Text = "ExcelExport";
			this.Load += new System.EventHandler(this.ExcelExport_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbkExportDiff;
        private System.Windows.Forms.CheckBox cbkExportComments;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
		  private System.Windows.Forms.CheckBox cbkIncludeProjectsWithoutTranslations;
    }
}