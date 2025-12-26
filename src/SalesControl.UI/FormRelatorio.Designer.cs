namespace SalesControl.UI
{
    partial class FormRelatorio
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
            panelTopFilters = new Panel();
            btnGenerate = new Button();
            dtpEnd = new DateTimePicker();
            lblEnd = new Label();
            dtpStart = new DateTimePicker();
            lblStart = new Label();
            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            panelTopFilters.SuspendLayout();
            SuspendLayout();
            // 
            // panelTopFilters
            // 
            panelTopFilters.Controls.Add(btnGenerate);
            panelTopFilters.Controls.Add(dtpEnd);
            panelTopFilters.Controls.Add(lblEnd);
            panelTopFilters.Controls.Add(dtpStart);
            panelTopFilters.Controls.Add(lblStart);
            panelTopFilters.Dock = DockStyle.Top;
            panelTopFilters.Location = new Point(0, 0);
            panelTopFilters.Name = "panelTopFilters";
            panelTopFilters.Size = new Size(684, 40);
            panelTopFilters.TabIndex = 0;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(310, 6);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(80, 26);
            btnGenerate.TabIndex = 5;
            btnGenerate.Text = "Gerar";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(184, 6);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(110, 23);
            dtpEnd.TabIndex = 4;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(154, 10);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(28, 15);
            lblEnd.TabIndex = 3;
            lblEnd.Text = "Até:";
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(36, 6);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(110, 23);
            dtpStart.TabIndex = 2;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(8, 10);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(24, 15);
            lblStart.TabIndex = 1;
            lblStart.Text = "De:";
            // 
            // reportViewer
            // 
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 40);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(684, 421);
            reportViewer.TabIndex = 0;
            // 
            // FormRelatorio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(reportViewer);
            Controls.Add(panelTopFilters);
            Name = "FormRelatorio";
            Text = "FormRelatorio";
            Load += FormRelatorio_Load;
            panelTopFilters.ResumeLayout(false);
            panelTopFilters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTopFilters;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label lblStart;
        private Label lblEnd;
        private Button btnGenerate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}