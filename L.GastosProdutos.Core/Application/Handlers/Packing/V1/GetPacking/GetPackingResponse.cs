using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking
{
    public record GetPackingResponse
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );
}