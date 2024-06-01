namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.ById
{
    public record GetProductByIdResponse
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice
    );
}