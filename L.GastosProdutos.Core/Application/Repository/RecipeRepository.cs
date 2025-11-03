using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class RecipeRepository : GenericRepository<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext db) : base(db) { }

        public async Task<long> CountIngredientsAsync(string recipeId, CancellationToken cancellationToken = default)
        {
            var count = await _db.Recipes
                .Where(r => r.Id == recipeId)
                .Select(r => r.Ingredients.Count)
                .FirstOrDefaultAsync(cancellationToken);

            return count;
        }

        public override async Task UpdateAsync(string id, RecipeEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Packings)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken)
                ?? throw new L.GastosProdutos.Core.Application.Exceptions.NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);

            existing.RemoveAllIngredientsAndPackings();
            foreach (var ing in entity.Ingredients)
                existing.AddIngredient(ing);
            foreach (var pk in entity.Packings)
                existing.AddPacking(pk);

            await _db.SaveChangesAsync(cancellationToken);
        }

        // DeleteAsync inherited from GenericRepository
    }
}
