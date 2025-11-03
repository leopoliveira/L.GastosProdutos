using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IRecipeRepository : IRepository<RecipeEntity>
    {
        Task<long> CountIngredientsAsync(string recipeId, CancellationToken cancellationToken = default);
    }
}
