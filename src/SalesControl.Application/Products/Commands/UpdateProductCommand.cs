using Ardalis.Result;
using MediatR;
using SalesControl.Application.Products.Dtos;
using SalesControl.Application.Products.Extensions;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;
using SalesControl.Application.Common;

namespace SalesControl.Application.Products.Commands
{
    public record UpdateProductCommand(Guid Id, string Name, string? Description, decimal Price, int Stock) : ICommand<Result<ProductOutput>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductOutput>>
    {
        private readonly IRepository<Product> _repository;
        public UpdateProductCommandHandler(IRepository<Product> repository) => _repository = repository;

        public async Task<Result<ProductOutput>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<ProductOutput>.Error("Invalid product id.");
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<ProductOutput>.Error("Name is required.");
            if (request.Price <= 0)
                return Result<ProductOutput>.Error("Price must be greater than zero.");
            if (request.Stock < 0)
                return Result<ProductOutput>.Error("Stock cannot be negative.");

            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return Result<ProductOutput>.Error("Product not found.");

            product.UpdateName(request.Name.Trim());
            product.UpdateDescription(request.Description?.Trim());
            product.UpdatePrice(request.Price);

            // Adjust stock by setting property via domain methods
            var currentStock = product.Stock;
            if (request.Stock > currentStock)
                product.IncreaseStock(request.Stock - currentStock);
            else if (request.Stock < currentStock)
                product.DecreaseStock(currentStock - request.Stock);

            await _repository.UpdateAsync(product, cancellationToken);
            return Result<ProductOutput>.Success(product.ToDto());
        }
    }
}
