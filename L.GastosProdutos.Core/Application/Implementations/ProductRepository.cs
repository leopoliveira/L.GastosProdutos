using L.GastosProdutos.Core.Application.Interfaces;
using L.GastosProdutos.Core.Domain.Entities.Product;

namespace L.GastosProdutos.Core.Application.Implementations
{
    public class ProductRepository : IProductRepository
    {
        public Task<IReadOnlyList<ProductEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
