using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle
        (
            UpdateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Product not found.");

            UpdateEntity(request, product);

            await _productRepository.UpdateAsync(request.Id, product);

            return Unit.Value;
        }

        private static void UpdateEntity
        (
            UpdateProductRequest request,
            ProductEntity product
        )
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
        }
    }
}
