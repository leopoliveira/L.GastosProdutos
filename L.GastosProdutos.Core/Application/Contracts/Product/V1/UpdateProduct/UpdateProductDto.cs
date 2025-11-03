using L.GastosProdutos.Core.Application.Contracts.Product;

namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct
{
    public record UpdateProductDto(
        string Name,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure)
        : ProductWriteDto(Name, Price, Quantity, UnitOfMeasure);
}

