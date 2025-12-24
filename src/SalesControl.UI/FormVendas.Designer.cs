namespace SalesControl.UI
{
    partial class FormVendas
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
            lblCliente = new Label();
            lblItensVenda = new Label();
            cmbClientes = new ComboBox();
            dgvItensVenda = new DataGridView();
            itemsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            clientIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            createdAtDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            updatedAtDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            totalDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            saleBindingSource = new BindingSource(components);
            button2 = new Button();
            button3 = new Button();
            lblProduto = new Label();
            cmbProdutos = new ComboBox();
            lblQuantidade = new Label();
            nudQuantidade = new NumericUpDown();
            btnAdicionarItem = new Button();
            lblTotal = new Label();
            lblTotalValor = new Label();
            btnCancelar = new Button();
            btnRegistrarVenda = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvItensVenda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)saleBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantidade).BeginInit();
            SuspendLayout();
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(30, 42);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(47, 15);
            lblCliente.TabIndex = 3;
            lblCliente.Text = "Cliente:";
            // 
            // lblItensVenda
            // 
            lblItensVenda.AutoSize = true;
            lblItensVenda.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblItensVenda.Location = new Point(30, 82);
            lblItensVenda.Name = "lblItensVenda";
            lblItensVenda.Size = new Size(112, 20);
            lblItensVenda.TabIndex = 4;
            lblItensVenda.Text = "Itens da Venda";
            // 
            // cmbClientes
            // 
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.FormattingEnabled = true;
            cmbClientes.Location = new Point(120, 42);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(350, 23);
            cmbClientes.TabIndex = 5;
            // 
            // dgvItensVenda
            // 
            dgvItensVenda.AllowUserToAddRows = false;
            dgvItensVenda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvItensVenda.AutoGenerateColumns = false;
            dgvItensVenda.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItensVenda.Columns.AddRange(new DataGridViewColumn[] { itemsDataGridViewTextBoxColumn, clientIdDataGridViewTextBoxColumn, createdAtDataGridViewTextBoxColumn, updatedAtDataGridViewTextBoxColumn, totalDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            dgvItensVenda.DataSource = saleBindingSource;
            dgvItensVenda.Location = new Point(30, 112);
            dgvItensVenda.Name = "dgvItensVenda";
            dgvItensVenda.ReadOnly = true;
            dgvItensVenda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItensVenda.Size = new Size(598, 200);
            dgvItensVenda.TabIndex = 10;
            // 
            // itemsDataGridViewTextBoxColumn
            // 
            itemsDataGridViewTextBoxColumn.DataPropertyName = "Items";
            itemsDataGridViewTextBoxColumn.HeaderText = "Items";
            itemsDataGridViewTextBoxColumn.Name = "itemsDataGridViewTextBoxColumn";
            itemsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clientIdDataGridViewTextBoxColumn
            // 
            clientIdDataGridViewTextBoxColumn.DataPropertyName = "ClientId";
            clientIdDataGridViewTextBoxColumn.HeaderText = "ClientId";
            clientIdDataGridViewTextBoxColumn.Name = "clientIdDataGridViewTextBoxColumn";
            clientIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdAtDataGridViewTextBoxColumn
            // 
            createdAtDataGridViewTextBoxColumn.DataPropertyName = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.HeaderText = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.Name = "createdAtDataGridViewTextBoxColumn";
            createdAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedAtDataGridViewTextBoxColumn
            // 
            updatedAtDataGridViewTextBoxColumn.DataPropertyName = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.HeaderText = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.Name = "updatedAtDataGridViewTextBoxColumn";
            updatedAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            totalDataGridViewTextBoxColumn.HeaderText = "Total";
            totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            totalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // saleBindingSource
            // 
            saleBindingSource.DataSource = typeof(Domain.SaleAggregate.Sale);
            // 
            // button2
            // 
            button2.Location = new Point(57, 464);
            button2.Name = "button2";
            button2.Size = new Size(155, 35);
            button2.TabIndex = 12;
            button2.Text = "Finalizar Venda";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(227, 464);
            button3.Name = "button3";
            button3.Size = new Size(155, 35);
            button3.TabIndex = 13;
            button3.Text = "Cancelar";
            button3.UseVisualStyleBackColor = true;
            // 
            // lblProduto
            // 
            lblProduto.AutoSize = true;
            lblProduto.Location = new Point(30, 332);
            lblProduto.Name = "lblProduto";
            lblProduto.Size = new Size(50, 15);
            lblProduto.TabIndex = 14;
            lblProduto.Text = "Produto";
            lblProduto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbProdutos
            // 
            cmbProdutos.FormattingEnabled = true;
            cmbProdutos.Location = new Point(92, 332);
            cmbProdutos.Name = "cmbProdutos";
            cmbProdutos.Size = new Size(221, 23);
            cmbProdutos.TabIndex = 15;
            // 
            // lblQuantidade
            // 
            lblQuantidade.AutoSize = true;
            lblQuantidade.Location = new Point(331, 332);
            lblQuantidade.Name = "lblQuantidade";
            lblQuantidade.Size = new Size(72, 15);
            lblQuantidade.TabIndex = 16;
            lblQuantidade.Text = "Quantidade:";
            // 
            // nudQuantidade
            // 
            nudQuantidade.Location = new Point(409, 332);
            nudQuantidade.Name = "nudQuantidade";
            nudQuantidade.Size = new Size(49, 23);
            nudQuantidade.TabIndex = 17;
            nudQuantidade.TextAlign = HorizontalAlignment.Right;
            // 
            // btnAdicionarItem
            // 
            btnAdicionarItem.Location = new Point(478, 325);
            btnAdicionarItem.Name = "btnAdicionarItem";
            btnAdicionarItem.Size = new Size(150, 35);
            btnAdicionarItem.TabIndex = 18;
            btnAdicionarItem.Text = "Adicionar Item";
            btnAdicionarItem.UseVisualStyleBackColor = true;
            btnAdicionarItem.Click += btnAdicionarItem_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(417, 382);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(127, 21);
            lblTotal.TabIndex = 19;
            lblTotal.Text = "Total da Venda:";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalValor
            // 
            lblTotalValor.AutoSize = true;
            lblTotalValor.BackColor = Color.LightYellow;
            lblTotalValor.BorderStyle = BorderStyle.FixedSingle;
            lblTotalValor.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalValor.Location = new Point(547, 382);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(25, 27);
            lblTotalValor.TabIndex = 20;
            lblTotalValor.Text = "0";
            lblTotalValor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancelar.Location = new Point(30, 375);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(150, 45);
            btnCancelar.TabIndex = 21;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnRegistrarVenda
            // 
            btnRegistrarVenda.Location = new Point(210, 375);
            btnRegistrarVenda.Name = "btnRegistrarVenda";
            btnRegistrarVenda.Size = new Size(180, 45);
            btnRegistrarVenda.TabIndex = 22;
            btnRegistrarVenda.Text = "Registrar Venda";
            btnRegistrarVenda.UseVisualStyleBackColor = true;
            btnRegistrarVenda.Click += btnRegistrarVenda_Click;
            // 
            // FormVendas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(btnRegistrarVenda);
            Controls.Add(btnCancelar);
            Controls.Add(lblTotalValor);
            Controls.Add(lblTotal);
            Controls.Add(btnAdicionarItem);
            Controls.Add(nudQuantidade);
            Controls.Add(lblQuantidade);
            Controls.Add(cmbProdutos);
            Controls.Add(lblProduto);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dgvItensVenda);
            Controls.Add(cmbClientes);
            Controls.Add(lblCliente);
            Controls.Add(lblItensVenda);
            Name = "FormVendas";
            Text = "Vendas";
            Load += FormVendas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvItensVenda).EndInit();
            ((System.ComponentModel.ISupportInitialize)saleBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantidade).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCliente;
        private Label lblItensVenda;
        private ComboBox cmbClientes;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Label label1;
        private DataGridView dgvItensVenda;
        private DataGridViewTextBoxColumn itemsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn clientIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn createdAtDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn updatedAtDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private BindingSource saleBindingSource;
        private Label label4;
        private Button button2;
        private Button button3;
        private Label lblProduto;
        private ComboBox cmbProdutos;
        private Label lblQuantidade;
        private NumericUpDown nudQuantidade;
        private Button btnAdicionarItem;
        private Label lblTotal;
        private Label lblTotalValor;
        private Button btnCancelar;
        private Button btnRegistrarVenda;
    }
}