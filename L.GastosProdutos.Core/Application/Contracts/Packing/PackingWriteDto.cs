namespace L.GastosProdutos.Core.Application.Contracts.Packing
{
    // Base write DTO reused across requests
    public record PackingWriteDto
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

