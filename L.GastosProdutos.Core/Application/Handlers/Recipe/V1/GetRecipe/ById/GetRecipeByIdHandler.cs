using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Handlers.Recipe.Helper;
using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe.ById
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdRequest, GetRecipeResponse>
    {
        private readonly IRecipeRepository _repository;

        public GetRecipeByIdHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetRecipeResponse> Handle
        (
            GetRecipeByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = await _repository.GetByIdAsync(request.RecipeId)
                ?? throw new NotFoundException("Recipe not found.");

            return RecipeMapping.MapEntityToResponse(recipe);
        }
    }
}
