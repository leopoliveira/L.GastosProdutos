using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class PackingRepository : GenericRepository<PackingEntity>, IPackingRepository
    {
        public PackingRepository(AppDbContext db) : base(db) { }
    }
}
