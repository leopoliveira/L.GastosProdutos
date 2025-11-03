using System.Linq.Expressions;
using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class PackingRepository : IPackingRepository
    {
        private readonly AppDbContext _db;

        public PackingRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<PackingEntity>> GetAllAsync() =>
            await _db.Packings.AsNoTracking().ToListAsync();

        public async Task<PackingEntity?> GetByIdAsync(string id) =>
            await _db.Packings.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IReadOnlyList<PackingEntity>> GetByFilterAsync(Expression<Func<PackingEntity, bool>> filter) =>
            await _db.Packings.AsNoTracking().Where(filter).ToListAsync();

        public async Task CreateAsync(PackingEntity entity)
        {
            _db.Packings.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, PackingEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}
