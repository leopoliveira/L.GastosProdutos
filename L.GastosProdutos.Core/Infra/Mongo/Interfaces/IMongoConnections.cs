using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infra.Mongo.Interfaces
{
    public interface IMongoConnections
    {
        IMongoDatabase GetDatabase();
    }
}
