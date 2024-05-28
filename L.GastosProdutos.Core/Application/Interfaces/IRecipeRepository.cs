using L.GastosProdutos.Core.Domain.Entities.Receipt;

namespace L.GastosProdutos.Core.Application.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IReadOnlyList<RecipeEntity>> GetAllAsync();

        Task<RecipeEntity> GetByIdAsync(string id);

        Task CreateAsync(RecipeEntity entity);

        Task UpdateAsync(string id, RecipeEntity entity);

        Task DeleteAsync(string id);
    }
}
