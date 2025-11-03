namespace L.GastosProdutos.Core.Application.Contracts.Packing
{
    public record PackingWriteDto
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

