using Ardalis.Result;
using MediatR;
using SalesControl.Application.Products.Dtos;
using SalesControl.Application.Products.Extensions;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ProductAggregate;
using SalesControl.Domain.ProductAggregate.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SalesControl.Application.Common;

namespace SalesControl.Application.Products.Queries
{
    public record GetProductsListQuery(string? Search) : IQuery<Result<IReadOnlyCollection<ProductOutput>>>;

    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, Result<IReadOnlyCollection<ProductOutput>>>
    {
        private readonly IReadRepository<Product> _repository;
        public GetProductsListQueryHandler(IReadRepository<Product> repository) => _repository = repository;

        public async Task<Result<IReadOnlyCollection<ProductOutput>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var products = string.IsNullOrWhiteSpace(request.Search)
                ? await _repository.ListAsync(cancellationToken: cancellationToken)
                : await _repository.ListAsync(new GetProductsByNameSpec(request.Search), cancellationToken);

            var dtos = products.Select(p => p.ToDto()).ToArray();
            return Result<IReadOnlyCollection<ProductOutput>>.Success(dtos);
        }
    }
}
