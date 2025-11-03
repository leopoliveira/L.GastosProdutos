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

        public async Task<IReadOnlyList<ProductEntity>> GetAllAsync() =>
            await _db.Products.AsNoTracking().ToListAsync();

        public async Task<ProductEntity> GetByIdAsync(string id) =>
            await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IReadOnlyList<ProductEntity>> GetByFilterAsync(Expression<Func<ProductEntity, bool>> filter) =>
            await _db.Products.AsNoTracking().Where(filter).ToListAsync();

        public async Task CreateAsync(ProductEntity entity)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, ProductEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}

