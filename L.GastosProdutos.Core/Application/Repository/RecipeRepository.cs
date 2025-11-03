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

        public async Task<IReadOnlyList<RecipeEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _db.Recipes.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<RecipeEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
            await _db.Recipes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        public async Task<long> CountIngredientsAsync(string recipeId, CancellationToken cancellationToken = default)
        {
            var count = await _db.Recipes
                .Where(r => r.Id == recipeId)
                .Select(r => r.Ingredients.Count)
                .FirstOrDefaultAsync(cancellationToken);

            return count;
        }

        public async Task CreateAsync(RecipeEntity entity, CancellationToken cancellationToken = default)
        {
            _db.Recipes.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(string id, RecipeEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            var existing = await _db.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Packings)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be updated.");

            entity.Id = existing.Id;
            _db.Entry(existing).CurrentValues.SetValues(entity);

            existing.RemoveAllIngredientsAndPackings();
            foreach (var ing in entity.Ingredients)
                existing.AddIngredient(ing);
            foreach (var pk in entity.Packings)
                existing.AddPacking(pk);

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Recipes.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
