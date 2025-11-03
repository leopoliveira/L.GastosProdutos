using System.Linq.Expressions;
using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _db;

        public RecipeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<RecipeEntity>> GetAllAsync() =>
            await _db.Recipes.AsNoTracking().ToListAsync();

        public async Task<RecipeEntity?> GetByIdAsync(string id) =>
            await _db.Recipes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

        public async Task<List<RecipeEntity>> GetByFilterAsync(Expression<Func<RecipeEntity, bool>> filter) =>
            await _db.Recipes.Where(filter).ToListAsync();

        public async Task<long> CountIngredientsAsync(string recipeId)
        {
            var count = await _db.Recipes
                .Where(r => r.Id == recipeId)
                .Select(r => r.Ingredients.Count)
                .FirstOrDefaultAsync();

            return count;
        }

        public async Task CreateAsync(RecipeEntity entity)
        {
            _db.Recipes.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, RecipeEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Packings)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);

            existing.RemoveAllIngredientsAndPackings();
            foreach (var ing in entity.Ingredients)
                existing.AddIngredient(ing);
            foreach (var pk in entity.Packings)
                existing.AddPacking(pk);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _db.Recipes.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}
