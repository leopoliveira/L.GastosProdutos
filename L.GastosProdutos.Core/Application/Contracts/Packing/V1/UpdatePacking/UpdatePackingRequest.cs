using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking
{
    public record UpdatePackingRequest
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );
}

