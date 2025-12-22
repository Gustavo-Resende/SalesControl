using SalesControl.Application.Products.Dtos;
using SalesControl.Domain.ProductAggregate;

namespace SalesControl.Application.Products.Extensions
{
    public static class ProductExtension
    {
        public static ProductOutput ToDto(this Product p)
            => new(p.Id, p.Name, p.Description, p.Price, p.Stock);
    }
}
