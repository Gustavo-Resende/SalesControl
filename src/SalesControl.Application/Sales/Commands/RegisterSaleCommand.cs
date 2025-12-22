using Ardalis.Result;
using MediatR;
using SalesControl.Application.Sales.DTOs;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace SalesControl.Application.Sales.Commands
{
    public record RegisterSaleCommand(RegisterSaleDto Payload) : ICommand<Result<Guid>>;

    public class RegisterSaleCommandHandler : IRequestHandler<RegisterSaleCommand, Result<Guid>>
    {
        private readonly ISalesService _salesService;

        public RegisterSaleCommandHandler(ISalesService salesService) => _salesService = salesService;

        public async Task<Result<Guid>> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.Payload is null)
                return Result<Guid>.Error("Payload is required.");

            try
            {
                var id = await _salesService.RegisterSaleAsync(request.Payload, cancellationToken);
                return Result<Guid>.Success(id);
            }
            catch (InvalidOperationException ex)
            {
                return Result<Guid>.Error(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Error("An unexpected error occurred while registering the sale.");
            }
        }
    }
}
