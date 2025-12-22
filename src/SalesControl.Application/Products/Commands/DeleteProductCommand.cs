using Ardalis.Result;
using MediatR;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SalesControl.Application.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : ICommand<Result<bool>>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IRepository<Product> _repository;
        public DeleteProductCommandHandler(IRepository<Product> repository) => _repository = repository;

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<bool>.Error("Invalid product id.");

            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return Result<bool>.Error("Product not found.");

            await _repository.DeleteAsync(product, cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
