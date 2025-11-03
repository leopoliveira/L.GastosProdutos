using L.GastosProdutos.Core.Application.Contracts.Packing;
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

    public record UpdatePackingDto(string Name, string? Description, decimal Price, decimal Quantity, int UnitOfMeasure)
        : PackingWriteDto(Name, Description, Price, Quantity, UnitOfMeasure);
}
