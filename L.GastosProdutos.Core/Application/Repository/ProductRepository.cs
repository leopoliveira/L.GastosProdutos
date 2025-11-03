using System.Linq.Expressions;
using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _db.Products.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<ProductEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
            await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task CreateAsync(ProductEntity entity, CancellationToken cancellationToken = default)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(string id, ProductEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
