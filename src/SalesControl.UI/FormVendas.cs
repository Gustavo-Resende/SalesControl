using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Queries;
using SalesControl.Application.Products.Dtos;
using SalesControl.Application.Products.Queries;
using SalesControl.Application.Sales.Commands;
using SalesControl.Application.Sales.DTOs;
using System.Data;

namespace SalesControl.UI
{
    public partial class FormVendas : Form
    {
        private readonly IMediator _mediator;

        private List<ClientOutput> _clientes = new();
        private List<ProductOutput> _produtos = new();

        private readonly List<RegisterSaleItemDto> _itensVenda = new();
        private decimal _totalVenda = 0m;

        public FormVendas(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            ConfigurarGridItens();

            nudQuantidade.ValueChanged += (s, e) => {};
        }

        private void ConfigurarGridItens()
        {
            dgvItensVenda.AutoGenerateColumns = false;
            dgvItensVenda.ReadOnly = true;
            dgvItensVenda.AllowUserToAddRows = false;
            dgvItensVenda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItensVenda.MultiSelect = false;

            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Produto",
                DataPropertyName = "ProductName",
                Width = 250
            });

            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantidade",
                DataPropertyName = "Quantity",
                Width = 100
            });

            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Preço Unit.",
                DataPropertyName = "UnitPrice",
                DefaultCellStyle = { Format = "C2" },
                Width = 120
            });

            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LineTotal",
                HeaderText = "Subtotal",
                DataPropertyName = "LineTotal",
                DefaultCellStyle = { Format = "C2" },
                Width = 140
            });
        }
        private async void FormVendas_Load(object sender, EventArgs e)
        {
            await CarregarClientesAsync();
            await CarregarProdutosAsync();
        }

        private async Task CarregarClientesAsync()
        {
            var result = await _mediator.Send(new GetClientsListQuery());
            if (result.IsSuccess)
            {
                _clientes = result.Value.ToList();
                cmbClientes.DataSource = _clientes;
                cmbClientes.DisplayMember = "Name";
                cmbClientes.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("Erro ao carregar clientes.");
            }
        }

        private async Task CarregarProdutosAsync()
        {
            var result = await _mediator.Send(new GetProductsListQuery());
            if (result.IsSuccess)
            {
                _produtos = result.Value.ToList();
                cmbProdutos.DataSource = _produtos;
                cmbProdutos.DisplayMember = "Name";
                cmbProdutos.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("Erro ao carregar produtos.");
            }
        }
        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (cmbProdutos.SelectedItem is not ProductOutput produto)
            {
                MessageBox.Show("Selecione um produto.");
                cmbProdutos.Focus();
                return;
            }

            int quantidade = (int)nudQuantidade.Value;
            if (quantidade <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que zero.");
                nudQuantidade.Focus();
                return;
            }

            var itemParaCommand = new RegisterSaleItemDto(
                ProductId: produto.Id,
                Quantity: quantidade
            );

            _itensVenda.Add(itemParaCommand);

            var itemParaDisplay = new
            {
                ProductName = produto.Name,
                Quantity = quantidade,
                UnitPrice = produto.Price,
                LineTotal = produto.Price * quantidade
            };

            var listaDisplay = dgvItensVenda.DataSource as List<object> ?? new List<object>();
            listaDisplay.Add(itemParaDisplay);
            dgvItensVenda.DataSource = null;
            dgvItensVenda.DataSource = listaDisplay;

            _totalVenda += produto.Price * quantidade;
            lblTotalValor.Text = _totalVenda.ToString("C2");

            nudQuantidade.Value = 0;
        }

        private async void btnRegistrarVenda_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedItem is not ClientOutput cliente)
            {
                MessageBox.Show("Selecione um cliente.");
                return;
            }

            if (!_itensVenda.Any())
            {
                MessageBox.Show("Adicione pelo menos um item.");
                return;
            }

            var payload = new RegisterSaleDto(
                ClientId: cliente.Id,
                Items: _itensVenda.AsReadOnly()
            );

            var command = new RegisterSaleCommand(payload);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                MessageBox.Show("Venda registrada com sucesso!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao registrar:\n" + string.Join("\n", result.Errors));
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
