namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking
{
    public record AddPackingRequest
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

