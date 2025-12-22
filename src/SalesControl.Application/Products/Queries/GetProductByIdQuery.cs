using Ardalis.Result;
using MediatR;
using SalesControl.Application.Products.Dtos;
using SalesControl.Application.Products.Extensions;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;
using SalesControl.Application.Common;

namespace SalesControl.Application.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IQuery<Result<ProductOutput>>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductOutput>>
    {
        private readonly IReadRepository<Product> _repository;
        public GetProductByIdQueryHandler(IReadRepository<Product> repository) => _repository = repository;

        public async Task<Result<ProductOutput>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<ProductOutput>.Error("Invalid product id.");

            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return Result<ProductOutput>.Error("Product not found.");

            return Result<ProductOutput>.Success(product.ToDto());
        }
    }
}
