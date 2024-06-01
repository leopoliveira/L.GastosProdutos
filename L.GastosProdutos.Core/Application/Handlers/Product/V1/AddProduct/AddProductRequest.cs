using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.AddProduct
{
    public record AddProductRequest
    (
        string Name,
        decimal Price,
        decimal Quantity
    )
    : IRequest<AddProductResponse>;
}
