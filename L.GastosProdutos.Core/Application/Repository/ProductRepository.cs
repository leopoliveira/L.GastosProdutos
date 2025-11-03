using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(AppDbContext db) : base(db) { }
    }
}
