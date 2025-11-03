namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.AddProduct
{
    public record AddProductRequest
    (
        string Name,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}
