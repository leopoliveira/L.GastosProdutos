using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using MongoDB.Driver;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductEntity>> GetAllAsync();

        Task<ProductEntity> GetByIdAsync(string id);

        Task<IReadOnlyList<ProductEntity>> GetByFilterAsync(Expression<Func<ProductEntity, bool>> filter);

        Task<IReadOnlyList<ProductEntity>> GetByFilterAsync(FilterDefinition<ProductEntity> filter);

        Task CreateAsync(ProductEntity entity);

        Task UpdateAsync(string id, ProductEntity entity);

        Task DeleteAsync(string id);
    }
}
