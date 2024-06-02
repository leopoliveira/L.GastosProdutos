using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.UpdateRecipe
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeRequest, Unit>
    {
        private readonly IRecipeRepository _repository;

        public UpdateRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle
        (
            UpdateRecipeRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = await _repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Recipe not found.");

            MapRequestToEntity(request, recipe);

            await _repository.UpdateAsync(request.Id, recipe);

            return Unit.Value;
        }

        private static void MapRequestToEntity(UpdateRecipeRequest request, RecipeEntity recipe)
        {
            recipe.Name = request.Name;
            recipe.Description = request.Description;
            recipe.RemoveAllIngredientsAndPackings();

            foreach (var ingredient in request.Ingredients)
            {
                recipe.AddIngredient
                (
                    new IngredientsValueObject
                    (
                        ingredient.ProductId,
                        ingredient.ProductName,
                        ingredient.Quantity,
                        ingredient.ProductUnitPrice
                    )
                );
            }

            foreach (var packing in request.Packings)
            {
                recipe.AddPacking
                (
                    new PackingValueObject
                    (
                        packing.PackingId,
                        packing.PackingName,
                        packing.Cost
                    )
                );
            }
        }
    }
}
