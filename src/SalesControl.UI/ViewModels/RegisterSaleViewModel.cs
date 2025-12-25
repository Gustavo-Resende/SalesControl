namespace SalesControl.UI.ViewModels
{
    public class RegisterSaleViewModel
    {
        public RegisterSaleViewModel() { }

        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public List<RegisterSaleItemViewModel> Items { get; set; } = new();
    }

    public class RegisterSaleItemViewModel
    {
        public RegisterSaleItemViewModel() { }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
