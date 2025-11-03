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

        public async Task<IReadOnlyList<PackingEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _db.Packings.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<PackingEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
            await _db.Packings.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task CreateAsync(PackingEntity entity, CancellationToken cancellationToken = default)
        {
            _db.Packings.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(string id, PackingEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
