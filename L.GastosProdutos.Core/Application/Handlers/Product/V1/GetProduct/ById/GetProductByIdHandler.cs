using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.ById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductResponse>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetProductResponse> Handle
        (
            GetProductByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _repository.GetByIdAsync(request.Id)
                 ?? throw new NotFoundException("Product not found");

            return new GetProductResponse
            (
                product.Id,
                product.Name,
                product.Price,
                product.Quantity,
                product.UnitPrice,
                product.UnitOfMeasure
            );
        }
    }
}
