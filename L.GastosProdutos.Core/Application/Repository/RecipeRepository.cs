using L.GastosProdutos.Core.Application.Exceptions;
using System.Linq.Expressions;

using MongoDB.Driver;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;
using L.GastosProdutos.Core.Infra.Mongo.MongoCollections;
using L.GastosProdutos.Core.Infra.Mongo.Interfaces;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IMongoCollection<RecipeEntity> _collection;

        public RecipeRepository(IMongoContext context)
        {
            _collection = context.GetCollection<RecipeEntity>(MongoCollectionsNames.RECIPE);
        }

        public async Task<IReadOnlyList<RecipeEntity>> GetAllAsync() =>
            await _collection
            .Find(r => !r.IsDeleted)
            .ToListAsync();

        public async Task<RecipeEntity> GetByIdAsync(string id) =>
            await _collection
            .Find(r => r.Id == id && !r.IsDeleted)
            .FirstOrDefaultAsync();

        public async Task<List<RecipeEntity>> GetByFilterAsync(Expression<Func<RecipeEntity, bool>> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<List<RecipeEntity>> GetByFilterAsync(FilterDefinition<RecipeEntity> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<long> CountIngredientsAsync(string recipeId) =>
            await _collection.CountDocumentsAsync(r => r.Id == recipeId && !r.IsDeleted);

        public async Task CreateAsync(RecipeEntity entity) =>
            await _collection
            .InsertOneAsync(entity);

        public async Task UpdateAsync(string id, RecipeEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _collection
                .ReplaceOneAsync(r => r.Id == id && !r.IsDeleted, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id) ??
                throw new NotFoundException("Entity not found. Nothing will be deleted.");

            entity.IsDeleted = true;

            await UpdateAsync(id, entity);
        }
    }
}
