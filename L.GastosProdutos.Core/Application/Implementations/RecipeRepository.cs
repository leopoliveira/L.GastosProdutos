using L.GastosProdutos.Core.Application.Interfaces;
using L.GastosProdutos.Core.Domain.Entities.Receipt;

namespace L.GastosProdutos.Core.Application.Implementations
{
    public class RecipeRepository : IRecipeRepository
    {
        public Task<IReadOnlyList<RecipeEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeEntity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(RecipeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, RecipeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
