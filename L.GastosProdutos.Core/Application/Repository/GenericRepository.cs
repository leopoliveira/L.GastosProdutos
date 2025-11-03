using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _set;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _set = _db.Set<T>();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _set.AsNoTracking().ToListAsync(cancellationToken);

        public virtual async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
            => await _set.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public virtual async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _set.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _set.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _set.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}

