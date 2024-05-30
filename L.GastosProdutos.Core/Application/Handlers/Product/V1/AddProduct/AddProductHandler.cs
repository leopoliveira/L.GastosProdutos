using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.MediatR.Product.V1.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
    {
        private readonly IProductRepository _repository;

        public AddProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddProductResponse> Handle
        (
            AddProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = new ProductEntity(request.Name, request.Price, request.Quantity);

            await _repository.CreateAsync(product);

            return new AddProductResponse(product.Id);
        }
    }
}
