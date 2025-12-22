using System;
using System.Collections.Generic;

namespace SalesControl.Application.Sales.DTOs
{
    public record RegisterSaleItemDto(Guid ProductId, int Quantity);

    public record RegisterSaleDto(Guid ClientId, IReadOnlyCollection<RegisterSaleItemDto> Items);
}
