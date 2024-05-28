using L.GastosProdutos.Core.Domain.Entities.Product;

namespace L.GastosProdutos.Core.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductEntity>> GetAllAsync();

        Task<ProductEntity> GetByIdAsync(string id);

        Task CreateAsync(ProductEntity entity);

        Task UpdateAsync(string id, ProductEntity entity);

        Task DeleteAsync(string id);
    }
}
