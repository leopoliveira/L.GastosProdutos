using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.DeleteProduct
{
    public record DeleteProductRequest(string Id)
        : IRequest<Unit>;
}
