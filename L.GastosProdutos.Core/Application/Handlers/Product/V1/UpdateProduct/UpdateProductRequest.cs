using L.GastosProdutos.Core.Domain.Enums;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct
{
    public record UpdateProductRequest
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    )
    : IRequest<Unit>;

    public record UpdateProductDto
    (
        string Name,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );
}
