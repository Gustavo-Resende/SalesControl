using MediatR;
using SalesControl.Application.Clients.Commands;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Queries;
using System.Data;

namespace SalesControl.UI
{
    public partial class FormClientes : Form
    {

        private readonly IMediator _mediator;
        private BindingSource bindingSourceClientes = new BindingSource();
        private ClientOutput? clienteSelecionado;

        public FormClientes(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            dgvClientes.DataSource = bindingSourceClientes;
            ConfigurarGrid();
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
        }

        private void ConfigurarGrid()
        {
            dgvClientes.AutoGenerateColumns = true;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
        }

        private async void ClienteForm_Load(object sender, EventArgs e)
        {
            await CarregarClientesAsync();
        }

        private async Task CarregarClientesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetClientsListQuery());
                if (result.IsSuccess)
                {
                    bindingSourceClientes.DataSource = result.Value.ToList();
                    LimparSelecao();
                }
                else
                {
                    MessageBox.Show("Falha ao carregar: " + string.Join("\n", result.Errors));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
            }

        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (clienteSelecionado == null) // Modo Novo
                {
                    var command = new RegisterClientCommand(
                        txtNome.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefone.Text.Trim()
                    );

                    var result = await _mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CarregarClientesAsync();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show(string.Join("\n", result.Errors.Select(er => er)), "Erro");
                    }
                }
                else // Modo Edição
                {
                    var command = new UpdateClientCommand(
                        clienteSelecionado.Id,
                        txtNome.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefone.Text.Trim()
                    );

                    var result = await _mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Cliente atualizado!", "Sucesso");
                        await CarregarClientesAsync();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show(string.Join("\n", result.Errors));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            if (clienteSelecionado == null)
            {
                MessageBox.Show("Selecione um cliente para excluir.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Confirma exclusão do cliente {clienteSelecionado.Name}?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                var command = new DeleteClientCommand(clienteSelecionado.Id);
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    MessageBox.Show("Cliente excluído com sucesso.");
                    await CarregarClientesAsync();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir: " + string.Join("\n", result.Errors));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                LimparSelecao();
                return;
            }

            var selecionado = dgvClientes.SelectedRows[0].DataBoundItem as ClientOutput;
            if (selecionado != null)
            {
                clienteSelecionado = selecionado;
                txtNome.Text = selecionado.Name;
                txtEmail.Text = selecionado.Email;
                txtTelefone.Text = selecionado.Phone;
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

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.");
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-mail é obrigatório.");
                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("E-mail inválido.");
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtNome.Focus();
        }

        private void LimparSelecao()
        {
            clienteSelecionado = null;
            dgvClientes.ClearSelection();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
