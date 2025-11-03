using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Application.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GetProductResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _db.Products.AsNoTracking().ToListAsync(cancellationToken);
            return products.Select(p => p.ToResponse());
        }

        public async Task<GetProductResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new NotFoundException("Product not found");
            return product.ToResponse();
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

            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);

            return new AddProductResponse(product.Id);
        }

        public async Task UpdateAsync(string id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Product not found");

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            existing.UnitOfMeasure = (EnumUnitOfMeasure)dto.UnitOfMeasure;

            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Product not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
