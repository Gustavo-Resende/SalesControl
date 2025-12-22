using System;
using System.Collections.Generic;

namespace SalesControl.Application.Sales.DTOs
{
    public record SaleItemDetailDto(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, decimal LineTotal);

    public record SaleDetailDto(Guid Id, Guid ClientId, DateTimeOffset CreatedAt, IReadOnlyCollection<SaleItemDetailDto> Items, decimal Total);
}
