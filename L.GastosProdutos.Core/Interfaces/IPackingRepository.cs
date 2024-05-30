using System.Collections.Generic;
using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using MongoDB.Driver;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IPackingRepository
    {
        Task<IReadOnlyList<PackingEntity>> GetAllAsync();

        Task<PackingEntity> GetByIdAsync(string id);

        Task<IReadOnlyList<PackingEntity>> GetByFilterAsync(Expression<Func<PackingEntity, bool>> filter);

        Task<IReadOnlyList<PackingEntity>> GetByFilterAsync(FilterDefinition<PackingEntity> filter);

        Task CreateAsync(PackingEntity entity);

        Task UpdateAsync(string id, PackingEntity entity);

        Task DeleteAsync(string id);
    }
}
