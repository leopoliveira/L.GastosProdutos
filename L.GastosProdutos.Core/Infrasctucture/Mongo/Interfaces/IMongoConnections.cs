using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces
{
    public interface IMongoConnections
    {
        IMongoDatabase GetDatabase();
    }
}
