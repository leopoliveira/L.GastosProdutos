using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.All
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductRequest, IEnumerable<GetProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetProductResponse>> Handle
        (
            GetAllProductRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var products = await _repository.GetAllAsync();

            return products
                .Select
                (p =>
                    new GetProductResponse
                    (
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Quantity,
                        p.UnitPrice,
                        p.UnitOfMeasure
                    )
                );
        }
    }
}
