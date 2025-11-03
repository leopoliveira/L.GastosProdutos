using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Packing
{
    public record PackingResponse
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    );
}

