using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}

