namespace SalesControl.UI
{
    partial class FormClientes
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
            components = new System.ComponentModel.Container();
            clientBindingSource = new BindingSource(components);
            button7 = new Button();
            lblNome = new Label();
            lblEmail = new Label();
            lblTelefone = new Label();
            txtNome = new TextBox();
            txtEmail = new TextBox();
            txtTelefone = new MaskedTextBox();
            btnNovo = new Button();
            btnSalvar = new Button();
            btnExcluir = new Button();
            btnLimpar = new Button();
            dgvClientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)clientBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // clientBindingSource
            // 
            clientBindingSource.DataSource = typeof(Domain.ClientAggregate.Client);
            clientBindingSource.CurrentChanged += ClienteForm_Load;
            // 
            // button7
            // 
            button7.Location = new Point(104, 464);
            button7.Name = "button7";
            button7.Size = new Size(307, 34);
            button7.TabIndex = 14;
            button7.Text = "Novo";
            button7.UseVisualStyleBackColor = true;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(30, 30);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(40, 15);
            lblNome.TabIndex = 15;
            lblNome.Text = "Nome";
            lblNome.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(30, 70);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 15);
            lblEmail.TabIndex = 16;
            lblEmail.Text = "E-mail";
            lblEmail.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.Location = new Point(30, 110);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(52, 15);
            lblTelefone.TabIndex = 17;
            lblTelefone.Text = "Telefone";
            lblTelefone.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(120, 28);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(400, 23);
            txtNome.TabIndex = 18;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(120, 68);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 23);
            txtEmail.TabIndex = 19;
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(120, 108);
            txtTelefone.Mask = "(00) 00000-0000";
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(400, 23);
            txtTelefone.TabIndex = 21;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(121, 160);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(90, 30);
            btnNovo.TabIndex = 27;
            btnNovo.Text = "Novo";
            btnNovo.Click += btnNovo_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(238, 160);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(90, 30);
            btnSalvar.TabIndex = 23;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(356, 160);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(90, 30);
            btnExcluir.TabIndex = 24;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnLimpar
            // 
            btnLimpar.Location = new Point(474, 160);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(90, 30);
            btnLimpar.TabIndex = 25;
            btnLimpar.Text = "Limpar";
            btnLimpar.UseVisualStyleBackColor = true;
            btnLimpar.Click += btnLimpar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(30, 210);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.ScrollBars = ScrollBars.Horizontal;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(620, 230);
            dgvClientes.TabIndex = 26;
            // 
            // FormClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(dgvClientes);
            Controls.Add(btnLimpar);
            Controls.Add(btnExcluir);
            Controls.Add(btnSalvar);
            Controls.Add(btnNovo);
            Controls.Add(txtTelefone);
            Controls.Add(txtEmail);
            Controls.Add(txtNome);
            Controls.Add(lblTelefone);
            Controls.Add(lblEmail);
            Controls.Add(lblNome);
            Controls.Add(button7);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormClientes";
            Load += ClienteForm_Load;
            ((System.ComponentModel.ISupportInitialize)clientBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource clientBindingSource;
        private Button button7;
        private Label lblNome;
        private Label lblEmail;
        private Label lblTelefone;
        private TextBox txtNome;
        private TextBox txtEmail;
        private MaskedTextBox txtTelefone;
        private Button btnNovo;
        private Button btnSalvar;
        private Button btnExcluir;
        private Button btnLimpar;
        private DataGridView dgvClientes;
    }
}