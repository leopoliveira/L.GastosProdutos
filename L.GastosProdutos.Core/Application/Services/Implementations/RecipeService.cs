using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Application.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _db;

        public RecipeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GetRecipeResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var recipes = await _db.Recipes.AsNoTracking().ToListAsync(cancellationToken);
            return recipes.Select(r => r.ToResponse());
        }

        public async Task<GetRecipeResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var recipe = await _db.Recipes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
                ?? throw new NotFoundException("Recipe not found.");
            return recipe.ToResponse();
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

            _db.Recipes.Add(recipe);
            await _db.SaveChangesAsync(cancellationToken);

            return new AddRecipeResponse(recipe.Id);
        }

        public async Task UpdateAsync(string id, UpdateRecipeDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var recipe = await _db.Recipes.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Recipe not found.");

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

            recipe.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _db.Recipes.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Recipe not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);

        }

        // Mapping centralized in EntityToResponseMapper
    }
}
