using System;

namespace SalesControl.Application.Products.Dtos
{
    public record ProductOutput(
        Guid Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock
    );
}
