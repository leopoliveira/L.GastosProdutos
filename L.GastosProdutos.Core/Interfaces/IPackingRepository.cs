using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Packing;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IPackingRepository
    {
        Task<IReadOnlyList<PackingEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<PackingEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task CreateAsync(PackingEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(string id, PackingEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
