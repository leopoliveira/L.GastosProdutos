using MediatR;

namespace L.GastosProdutos.Core.Application.MediatR.Product.AddProduct
{
    public record AddProductRequest
    (
        string Name,
        decimal Price,
        decimal Quantity
    )
    : IRequest<AddProductResponse>;
}
