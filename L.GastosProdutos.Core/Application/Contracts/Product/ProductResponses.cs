using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Product
{
    public record ProductResponse
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    );
}

