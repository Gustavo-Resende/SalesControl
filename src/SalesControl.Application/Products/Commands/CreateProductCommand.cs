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
    public record CreateProductCommand(string Name, string? Description, decimal Price, int Stock) : ICommand<Result<ProductOutput>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductOutput>>
    {
        private readonly IRepository<Product> _repository;
        public CreateProductCommandHandler(IRepository<Product> repository) => _repository = repository;

        public async Task<Result<ProductOutput>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<ProductOutput>.Error("Name is required.");
            if (request.Price < 0)
                return Result<ProductOutput>.Error("Price must be greater than zero.");
            if (request.Stock <= 0)
                return Result<ProductOutput>.Error("Stock must be positive.");

            var product = new Product(request.Name.Trim(), request.Price, request.Stock, request.Description?.Trim());
            await _repository.AddAsync(product, cancellationToken);
            return Result<ProductOutput>.Success(product.ToDto());
        }
    }
}
