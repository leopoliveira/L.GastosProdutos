using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct
{
    public record UpdateProductRequest
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );

    public record UpdateProductDto
    (
        string Name,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}
