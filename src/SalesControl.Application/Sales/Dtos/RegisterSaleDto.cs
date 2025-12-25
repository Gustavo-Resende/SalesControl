using System;
using System.Collections.Generic;

namespace SalesControl.Application.Sales.DTOs
{
    public record RegisterSaleItemDto(Guid ProductId, int Quantity);

    public record RegisterSaleDto(Guid ClientId, IReadOnlyCollection<RegisterSaleItemDto> Items);

    public class SaleReportRowDto
    {
        public Guid SaleId { get; set; }
        public DateTimeOffset SaleDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
