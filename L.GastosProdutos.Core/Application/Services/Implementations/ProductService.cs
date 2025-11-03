using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetProductResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return products.Select(p => new GetProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.Quantity,
                p.UnitPrice,
                p.UnitOfMeasure
            ));
        }

        public async Task<GetProductResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Product not found");
            return new GetProductResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Quantity,
                product.UnitPrice,
                product.UnitOfMeasure
            );
        }

        public async Task<AddProductResponse> AddAsync(AddProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = new ProductEntity(
                request.Name,
                request.Price,
                request.Quantity,
                (EnumUnitOfMeasure)request.UnitOfMeasure
            );

            await _repository.CreateAsync(product, cancellationToken);

            return new AddProductResponse(product.Id);
        }

        public async Task UpdateAsync(string id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existing = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Product not found");

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            existing.UnitOfMeasure = (EnumUnitOfMeasure)dto.UnitOfMeasure;

            await _repository.UpdateAsync(id, existing, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}
