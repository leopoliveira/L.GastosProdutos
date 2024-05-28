using L.GastosProdutos.Core.Domain.Entities.Packing;

namespace L.GastosProdutos.Core.Application.Interfaces
{
    public interface IPackingRepository
    {
        Task<IReadOnlyList<PackingEntity>> GetAllAsync();

        Task<PackingEntity> GetByIdAsync(string id);

        Task CreateAsync(PackingEntity entity);

        Task UpdateAsync(string id, PackingEntity entity);

        Task DeleteAsync(string id);
    }
}
