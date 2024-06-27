using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.AddRecipe
{
    public class AddRecipeHandler : IRequestHandler<AddRecipeRequest, AddRecipeResponse>
    {
        private readonly IRecipeRepository _repository;

        public AddRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddRecipeResponse> Handle
        (
            AddRecipeRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = new RecipeEntity
            (
                request.Name,
                request.Description,
                new List<IngredientsValueObject>(),
                new List<PackingValueObject>()
            );

            if (request.Ingredients.Count > 0)
            {
                foreach (var ingredient in request.Ingredients)
                {
                    recipe.AddIngredient
                    (
                        new IngredientsValueObject
                        (
                            ingredient.ProductId,
                            ingredient.ProductName,
                            ingredient.Quantity,
                            ingredient.IngredientPrice
                        )
                    );
                }
            }

            if (request.Packings.Count > 0)
            {
                foreach (var packing in request.Packings)
                {
                    recipe.AddPacking
                    (
                        new PackingValueObject
                        (
                            packing.PackingId,
                            packing.PackingName,
                            packing.Quantity,
                            packing.PackingUnitPrice
                        )
                    );
                }
            }

            await _repository.CreateAsync(recipe);

            return new AddRecipeResponse(recipe.Id);
        }
    }
}
