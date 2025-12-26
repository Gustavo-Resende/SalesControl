using SalesControl.Application.Sales.DTOs;
using SalesControl.UI.ViewModels;
using System.Linq;

namespace SalesControl.UI.Extensions
{
    public static class ViewModelExtensions
    {
        public static RegisterSaleViewModel ToViewModel(this RegisterSaleDto dto, Func<Guid, string?> clientNameResolver, Func<Guid, (string Name, decimal Price)?> productResolver)
        {
            var vm = new RegisterSaleViewModel
            {
                ClientId = dto.ClientId,
                ClientName = clientNameResolver(dto.ClientId) ?? string.Empty,
                Items = dto.Items.Select(i =>
                {
                    var prod = productResolver(i.ProductId);
                    return new RegisterSaleItemViewModel
                    {
                        ProductId = i.ProductId,
                        ProductName = prod?.Name ?? string.Empty,
                        UnitPrice = prod?.Price ?? 0m,
                        Quantity = i.Quantity
                    };
                }).ToList()
            };

            return vm;
        }
    }
}
