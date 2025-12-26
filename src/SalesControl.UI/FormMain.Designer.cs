namespace SalesControl.UI
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClients = new Button();
            btnProduct = new Button();
            btnSale = new Button();
            label1 = new Label();
            label2 = new Label();
            btnReport = new Button();
            SuspendLayout();
            // 
            // btnClients
            // 
            btnClients.Location = new Point(193, 192);
            btnClients.Name = "btnClients";
            btnClients.Size = new Size(298, 34);
            btnClients.TabIndex = 1;
            btnClients.Text = "Clientes";
            btnClients.UseVisualStyleBackColor = true;
            btnClients.Click += btnClients_Click;
            // 
            // btnProduct
            // 
            btnProduct.Location = new Point(193, 238);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(298, 34);
            btnProduct.TabIndex = 2;
            btnProduct.Text = "Produtos";
            btnProduct.UseVisualStyleBackColor = true;
            btnProduct.Click += btnProduct_Click;
            // 
            // btnSale
            // 
            btnSale.Location = new Point(193, 284);
            btnSale.Name = "btnSale";
            btnSale.Size = new Size(298, 34);
            btnSale.TabIndex = 3;
            btnSale.Text = "Vendas";
            btnSale.UseVisualStyleBackColor = true;
            btnSale.Click += btnSale_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(337, 126);
            label1.Name = "label1";
            label1.Size = new Size(10, 15);
            label1.TabIndex = 0;
            label1.Text = " ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(205, 104);
            label2.Name = "label2";
            label2.Size = new Size(286, 37);
            label2.TabIndex = 6;
            label2.Text = "SISTEMA DE VENDAS";
            // 
            // btnReport
            // 
            btnReport.Location = new Point(193, 334);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(298, 34);
            btnReport.TabIndex = 7;
            btnReport.Text = "Relatorio";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(btnReport);
            Controls.Add(label2);
            Controls.Add(btnSale);
            Controls.Add(btnProduct);
            Controls.Add(btnClients);
            Controls.Add(label1);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            Load += Main_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClients;
        private Button btnProduct;
        private Button btnSale;
        private Label label1;
        private Label label2;
        private Button btnReport;
    }
}
