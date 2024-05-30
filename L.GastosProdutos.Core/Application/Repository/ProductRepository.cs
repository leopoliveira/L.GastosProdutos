using L.GastosProdutos.Core.Application.Exceptions;
using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.MongoCollections;

using MongoDB.Driver;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductEntity> _collection;

        public ProductRepository(IMongoContext context)
        {
            _collection = context.GetCollection<ProductEntity>(MongoCollectionsNames.PRODUCT);
        }

        public async Task<IReadOnlyList<ProductEntity>> GetAllAsync() =>
            await _collection
            .Find(_ => true)
            .ToListAsync();

        public async Task<ProductEntity> GetByIdAsync(string id) =>
            await _collection
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();

        public async Task<IReadOnlyList<ProductEntity>> GetByFilterAsync(Expression<Func<ProductEntity, bool>> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<IReadOnlyList<ProductEntity>> GetByFilterAsync(FilterDefinition<ProductEntity> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task CreateAsync(ProductEntity entity) =>
            await _collection
            .InsertOneAsync(entity);

        public async Task UpdateAsync(string id, ProductEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _collection
                .ReplaceOneAsync(p => p.Id == id, entity);
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
