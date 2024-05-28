using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces
{
    public interface IMongoContext
    {
        IMongoDatabase GetDatabase();

        IMongoCollection<T> GetCollection<T>(string? name = null);
    }
}
