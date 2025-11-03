using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Recipe;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IReadOnlyList<RecipeEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<RecipeEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<long> CountIngredientsAsync(string recipeId, CancellationToken cancellationToken = default);

        Task CreateAsync(RecipeEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(string id, RecipeEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
