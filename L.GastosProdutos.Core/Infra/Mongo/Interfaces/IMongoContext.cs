using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infra.Mongo.Interfaces
{
    public interface IMongoContext
    {
        IMongoDatabase GetDatabase();

        IMongoCollection<T> GetCollection<T>(string? name = null);
    }
}
