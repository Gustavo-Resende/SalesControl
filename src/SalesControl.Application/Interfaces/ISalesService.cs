using System;
using System.Threading;
using System.Threading.Tasks;
using SalesControl.Application.Sales.DTOs;

namespace SalesControl.Application.Interfaces
{
    public interface ISalesService
    {
        Task<Guid> RegisterSaleAsync(RegisterSaleDto dto, CancellationToken cancellationToken = default);

        Task<SaleDetailDto?> GetSaleByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
        
        Task<System.Collections.Generic.List<SaleReportRowDto>> GetSalesReportAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
    }
}