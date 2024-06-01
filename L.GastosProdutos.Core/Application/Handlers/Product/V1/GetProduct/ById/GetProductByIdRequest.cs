using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.ById
{
    public record GetProductByIdRequest(string Id)
        : IRequest<GetProductByIdResponse>;
}
