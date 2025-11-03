using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking
{
    public record GetPackingResponse
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
