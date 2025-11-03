using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Product;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ProductEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task CreateAsync(ProductEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(string id, ProductEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
