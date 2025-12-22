using System;
using System.Collections.Generic;
using System.Text;

namespace SalesControl.Application.Interfaces
{
    internal interface ISalesService
    {
        Task<Guid> RegisterSaleAsync(RegisterSaleDto dto, CancellationToken cancellationToken = default);
    }
}
