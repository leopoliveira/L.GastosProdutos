using System.Reflection.Metadata.Ecma335;

using L.GastosProdutos.Core.Application.Handlers.Recipe.Helper;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe.All
{
    public class GetAllRecipeHandler : IRequestHandler<GetAllRecipeRequest, IEnumerable<GetRecipeResponse>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<GetRecipeResponse>> Handle
        (
            GetAllRecipeRequest request,
            CancellationToken cancellationToken
        )
        {
            var recipes = await _recipeRepository.GetAllAsync();

            return recipes
                .Select(RecipeMapping.MapEntityToResponse);
        }
    }
}
