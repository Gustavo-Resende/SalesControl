using Ardalis.Result;
using MediatR;
using SalesControl.Application.Common;
using SalesControl.Application.Sales.DTOs;
using SalesControl.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace SalesControl.Application.Sales.Queries
{
    public record GetSalesReportQuery(DateTime Start, DateTime End) : IQuery<Result<List<SaleReportRowDto>>>;

    public class GetSalesReportQueryHandler : IRequestHandler<GetSalesReportQuery, Result<List<SaleReportRowDto>>>
    {
        private readonly ISalesService _salesService;
        public GetSalesReportQueryHandler(ISalesService salesService) => _salesService = salesService;

        public async Task<Result<List<SaleReportRowDto>>> Handle(GetSalesReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rows = await _salesService.GetSalesReportAsync(request.Start, request.End, cancellationToken);
                return Result<List<SaleReportRowDto>>.Success(rows);
            }
            catch (Exception ex)
            {
                // Propagate exception message for debugging; production code may log instead
                return Result<List<SaleReportRowDto>>.Error(ex.Message);
            }
        }
    }
}
