using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetRecipeResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var recipes = await _repository.GetAllAsync(cancellationToken);
            return recipes.Select(MapEntityToResponse);
        }

        public async Task<GetRecipeResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var recipe = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Recipe not found.");
            return MapEntityToResponse(recipe);
        }

        public async Task<AddRecipeResponse> AddAsync(AddRecipeRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = new RecipeEntity(
                request.Name,
                request.Description,
                new List<IngredientsValueObject>(),
                new List<PackingValueObject>(),
                request.Quantity,
                request.SellingValue
            );

            if (request.Ingredients.Count > 0)
            {
                foreach (var ingredient in request.Ingredients)
                {
                    recipe.AddIngredient(new IngredientsValueObject(
                        ingredient.ProductId,
                        ingredient.ProductName,
                        ingredient.Quantity,
                        ingredient.IngredientPrice
                    ));
                }
            }

            if (request.Packings.Count > 0)
            {
                foreach (var packing in request.Packings)
                {
                    recipe.AddPacking(new PackingValueObject(
                        packing.PackingId,
                        packing.PackingName,
                        packing.Quantity,
                        packing.PackingUnitPrice
                    ));
                }
            }

            await _repository.CreateAsync(recipe, cancellationToken);

            return new AddRecipeResponse(recipe.Id);
        }

        public async Task UpdateAsync(string id, UpdateRecipeDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Recipe not found.");

            recipe.Name = dto.Name;
            recipe.Description = dto.Description;
            recipe.Quantity = dto.Quantity;
            recipe.SellingValue = dto.SellingValue;
            recipe.RemoveAllIngredientsAndPackings();

            foreach (var ingredient in dto.Ingredients)
            {
                recipe.AddIngredient(new IngredientsValueObject(
                    ingredient.ProductId,
                    ingredient.ProductName,
                    ingredient.Quantity,
                    ingredient.IngredientPrice
                ));
            }

            foreach (var packing in dto.Packings)
            {
                recipe.AddPacking(new PackingValueObject(
                    packing.PackingId,
                    packing.PackingName,
                    packing.Quantity,
                    packing.PackingUnitPrice
                ));
            }

            await _repository.UpdateAsync(id, recipe, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.DeleteAsync(id, cancellationToken);

        }

        private static GetRecipeResponse MapEntityToResponse(RecipeEntity recipe)
        {
            return new GetRecipeResponse
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
                        i.IngredientPrice
                    )),
                recipe.Packings.ConvertAll(p =>
                    new PackingDto
                    (
                        p.PackingId,
                        p.PackingName,
                        p.Quantity,
                        p.UnitPrice
                    )),
                recipe.TotalCost,
                recipe.Quantity ?? 0,
                recipe.SellingValue ?? 0
            );
        }
    }
}
