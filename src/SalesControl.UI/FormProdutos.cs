using Ardalis.Result;
using MediatR;
using SalesControl.Application.Products.Commands;
using SalesControl.Application.Products.Dtos;
using SalesControl.Application.Products.Queries;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesControl.UI
{
    public partial class FormProdutos : Form
    {
        private readonly IMediator _mediator;
        private readonly BindingSource bindingSourceProdutos = new BindingSource();

        private ProductOutput? produtoSelecionado;

        public FormProdutos(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            // Vincula o grid ao BindingSource
            dgvProdutos.DataSource = bindingSourceProdutos;

            ConfigurarGrid();
            dgvProdutos.SelectionChanged += dgvProdutos_SelectionChanged;
        }

        private void ConfigurarGrid()
        {
            dgvProdutos.AutoGenerateColumns = true;
            dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProdutos.ReadOnly = true;
            dgvProdutos.AllowUserToAddRows = false;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutos.MultiSelect = false;

        }

        private async void FormProdutos_Load(object sender, EventArgs e)
        {
            await CarregarProdutosAsync();
        }

        private async Task CarregarProdutosAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetProductsListQuery());
                if (result.IsSuccess)
                {
                    bindingSourceProdutos.DataSource = result.Value.ToList();
                    LimparSelecao();
                }
                else
                {
                    MessageBox.Show("Falha ao carregar produtos:\n" +
                                   string.Join("\n", result.Errors.Select(e => e)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                decimal preco;
                int estoque;

                if (!decimal.TryParse(txtPreco.Text.Replace(",", "."), out preco) || preco <= 0)
                {
                    MessageBox.Show("Preço deve ser maior que zero.");
                    txtPreco.Focus();
                    return;
                }

                if (!int.TryParse(txtEstoque.Text, out estoque) || estoque < 0)
                {
                    MessageBox.Show("Estoque não pode ser negativo.");
                    txtEstoque.Focus();
                    return;
                }

                if (produtoSelecionado == null) // Novo produto
                {
                    var command = new CreateProductCommand(
                        txtNome.Text.Trim(),
                        txtDescricao.Text.Trim(),
                        preco,
                        estoque
                    );

                    var result = await _mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso");
                        await CarregarProdutosAsync();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar:\n" +
                                       string.Join("\n", result.Errors.Select(e => e)));
                    }
                }
                else
                {
                    var command = new UpdateProductCommand(
                        produtoSelecionado.Id,
                        txtNome.Text.Trim(),
                        txtDescricao.Text.Trim(),
                        preco,
                        estoque
                    );

                    var result = await _mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Produto atualizado com sucesso!", "Sucesso");
                        await CarregarProdutosAsync();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar:\n" +
                                       string.Join("\n", result.Errors.Select(e => e)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar produto: " + ex.Message);
            }
        }

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            if (produtoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto na lista para excluir.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Confirma exclusão do produto \"{produtoSelecionado.Name}\" (ID: {produtoSelecionado.Id})?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                var result = await _mediator.Send(new DeleteProductCommand(produtoSelecionado.Id));

                if (result.IsSuccess)
                {
                    MessageBox.Show("Produto excluído com sucesso!");
                    await CarregarProdutosAsync();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir:\n" +
                                   string.Join("\n", result.Errors.Select(e => e)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            LimparSelecao();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvProdutos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                LimparSelecao();
                return;
            }

            var selecionado = dgvProdutos.SelectedRows[0].DataBoundItem as ProductOutput;
            if (selecionado != null)
            {
                produtoSelecionado = selecionado;
                txtNome.Text = selecionado.Name;
                txtDescricao.Text = selecionado.Description ?? "";
                txtPreco.Text = selecionado.Price.ToString("N2");
                txtEstoque.Text = selecionado.Stock.ToString();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.");
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Preço é obrigatório.");
                txtPreco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEstoque.Text))
            {
                MessageBox.Show("Estoque é obrigatório.");
                txtEstoque.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            txtNome.Focus();
        }

        private void LimparSelecao()
        {
            produtoSelecionado = null;
            dgvProdutos.ClearSelection();
        }

        private void FormProdutos_Load_1(object sender, EventArgs e)
        {

        }
    }
}