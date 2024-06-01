using L.GastosProdutos.Core.Infra.Mongo.Interfaces;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infra.Mongo.Settings
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoConnections _mongoConnections;

        public MongoContext(IMongoConnections mongoConnections)
        {
            _mongoConnections = mongoConnections;
        }

        public IMongoDatabase GetDatabase() =>
            _mongoConnections.GetDatabase();

        public IMongoCollection<T> GetCollection<T>(string? name = null)
        {
            var database = GetDatabase();

            return !string.IsNullOrWhiteSpace(name) ?
                database.GetCollection<T>(name) :
                GetCollection<T>(database);
        }

        private static IMongoCollection<T> GetCollection<T>(IMongoDatabase database)
        {
            var collectionName = typeof(T).Name;

            return database.GetCollection<T>(collectionName);
        }
    }
}
