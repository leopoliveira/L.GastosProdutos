﻿using System.Linq.Expressions;

using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Infra.Mongo.Interfaces;
using L.GastosProdutos.Core.Infra.Mongo.MongoCollections;
using L.GastosProdutos.Core.Interfaces;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Application.Repository
{
    public class PackingRepository : IPackingRepository
    {
        private readonly IMongoCollection<PackingEntity> _collection;

        public PackingRepository(IMongoContext context)
        {
            _collection = context.GetCollection<PackingEntity>(MongoCollectionsNames.PACKING);
        }

        public async Task<IReadOnlyList<PackingEntity>> GetAllAsync() =>
            await _collection
            .Find(p => !p.IsDeleted)
            .ToListAsync();

        public async Task<PackingEntity> GetByIdAsync(string id) =>
            await _collection
            .Find(p => p.Id == id && !p.IsDeleted)
            .FirstOrDefaultAsync();

        public async Task<IReadOnlyList<PackingEntity>> GetByFilterAsync(Expression<Func<PackingEntity, bool>> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task<IReadOnlyList<PackingEntity>> GetByFilterAsync(FilterDefinition<PackingEntity> filter) =>
            await _collection
            .Find(filter)
            .ToListAsync();

        public async Task CreateAsync(PackingEntity entity) =>
            await _collection
            .InsertOneAsync(entity);

        public async Task UpdateAsync(string id, PackingEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _collection
                .ReplaceOneAsync(p => p.Id == id && !p.IsDeleted, entity);
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
