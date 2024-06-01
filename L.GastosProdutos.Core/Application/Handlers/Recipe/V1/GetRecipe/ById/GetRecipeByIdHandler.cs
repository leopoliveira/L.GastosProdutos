using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe.ById
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdRequest, GetRecipeByIdResponse>
    {
        private readonly IRecipeRepository _repository;

        public GetRecipeByIdHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetRecipeByIdResponse> Handle
        (
            GetRecipeByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = await _repository.GetByIdAsync(request.RecipeId)
                ?? throw new NotFoundException("Recipe not found.");

            return MapEntityToResponse(recipe);
        }

        private static GetRecipeByIdResponse MapEntityToResponse(RecipeEntity recipe)
        {
            return new GetRecipeByIdResponse
            (
                recipe.Id,
                recipe.Name,
                recipe.Description,
                recipe.Ingredients.ConvertAll(i =>
                new IngredientDto
                (
                    i.ProductId,
                    i.ProductName,
                    i.Quantity,
                    i.ProductUnitPrice
                )),
                recipe.Packings.ConvertAll(p =>
                new PackingDto
                (
                    p.PackingId,
                    p.PackingName,
                    p.Cost
                )),
                recipe.TotalCost
            );
        }
    }
}
