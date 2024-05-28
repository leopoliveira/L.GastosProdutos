using L.GastosProdutos.Core.Application.Exceptions;
using System.Linq.Expressions;

using L.GastosProdutos.Core.Application.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.MongoCollections;

using MongoDB.Driver;
using L.GastosProdutos.Core.Domain.Entities.Recipe;

namespace L.GastosProdutos.Core.Application.Implementations
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
            .Find(_ => true)
            .ToListAsync();

        public async Task<RecipeEntity> GetByIdAsync(string id) =>
            await _collection
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();

        public async Task<IReadOnlyList<RecipeEntity>> GetByFilterAsync(Expression<Func<RecipeEntity, bool>> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<IReadOnlyList<RecipeEntity>> GetByFilterAsync(FilterDefinition<RecipeEntity> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<long> CountIngredientsAsync(string recipeId) =>
            await _collection.CountDocumentsAsync(r => r.Id == recipeId);

        public async Task CreateAsync(RecipeEntity entity) =>
            await _collection
            .InsertOneAsync(entity);

        public async Task UpdateAsync(string id, RecipeEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _collection
                .ReplaceOneAsync(r => r.Id == id, entity);
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
