using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe;

namespace L.GastosProdutos.Core.Application.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<GetRecipeResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetRecipeResponse>> GetAllAsync(string? groupId, CancellationToken cancellationToken);
        Task<GetRecipeResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<AddRecipeResponse> AddAsync(AddRecipeRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(string id, UpdateRecipeDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
