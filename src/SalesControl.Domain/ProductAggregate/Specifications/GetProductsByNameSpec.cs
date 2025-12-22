using Ardalis.Specification;
using System;

namespace SalesControl.Domain.ProductAggregate.Specifications
{
    public class GetProductsByNameSpec : Specification<Product>
    {
        public GetProductsByNameSpec(string name)
        {
            Query.Where(p => p.Name.Contains(name));
        }
    }
}
