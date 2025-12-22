using Ardalis.Result;
using MediatR;
using SalesControl.Application.Sales.DTOs;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace SalesControl.Application.Sales.Queries
{
    public record GetSaleByIdQuery(Guid SaleId) : IQuery<Result<SaleDetailDto>>;

    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Result<SaleDetailDto>>
    {
        private readonly ISalesService _salesService;
        public GetSaleByIdQueryHandler(ISalesService salesService) => _salesService = salesService;

        public async Task<Result<SaleDetailDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.SaleId == Guid.Empty)
                return Result<SaleDetailDto>.Error("Invalid sale id.");

            try
            {
                var dto = await _salesService.GetSaleByIdAsync(request.SaleId, cancellationToken);
                if (dto is null) return Result<SaleDetailDto>.Error("Sale not found.");
                return Result<SaleDetailDto>.Success(dto);
            }
            catch (Exception ex)
            {
                // Log exception in real application
                return Result<SaleDetailDto>.Error($"Unexpected error: {ex.Message}");
            }
        }
    }
}
