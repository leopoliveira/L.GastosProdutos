using L.GastosProdutos.Core.Application.Interfaces;
using L.GastosProdutos.Core.Domain.Entities.Packing;

namespace L.GastosProdutos.Core.Application.Implementations
{
    public class PackingRepository : IPackingRepository
    {
        public Task<IReadOnlyList<PackingEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PackingEntity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(PackingEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, PackingEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
