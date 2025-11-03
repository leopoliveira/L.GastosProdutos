namespace L.GastosProdutos.Core.Application.Contracts.Product
{
    // Canonical write DTO to reduce duplication across Add/Update
    public record ProductWriteDto
    (
        string Name,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

