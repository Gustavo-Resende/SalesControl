using MediatR;


namespace SalesControl.UI
{
    public partial class FormMain : Form
    {

        private readonly IMediator _mediator;

        public FormMain(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes(_mediator);
            formClientes.ShowDialog();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            var formProdutos = new FormProdutos(_mediator);
            formProdutos.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            var formVendas = new FormVendas(_mediator);
            formVendas.ShowDialog();
        }

        private void btnReport_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar: {ex.Message}");
            }
        }
    }
}
