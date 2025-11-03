using L.GastosProdutos.Core.Application.Contracts.Packing;

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking
{
    public record UpdatePackingDto(
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure)
        : PackingWriteDto(Name, Description, Price, Quantity, UnitOfMeasure);
}

