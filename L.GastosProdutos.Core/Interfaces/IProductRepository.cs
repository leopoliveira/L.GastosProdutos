using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        
    }
}
