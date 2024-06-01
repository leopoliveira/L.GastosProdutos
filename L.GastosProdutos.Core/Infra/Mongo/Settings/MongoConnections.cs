using L.GastosProdutos.Core.Infra.Mongo.Interfaces;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Infra.Mongo.Settings
{
    public class MongoConnections : IMongoConnections
    {
        private static readonly object Lock = new();

        private readonly Dictionary<string, IMongoDatabase> Databases = new();

        private readonly string _connectionString;

        private readonly string _defaultDatabaseName;

        public MongoConnections(IOptions<MongoSettings> options)
        {
            _connectionString = options.Value.ConnectionString;
            _defaultDatabaseName = options.Value.DatabaseName;
        }

        public IMongoDatabase GetDatabase()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new ArgumentNullException("Mongo Connection String is null.");

            if (string.IsNullOrWhiteSpace(_defaultDatabaseName))
                throw new ArgumentNullException("Mongo default database is null.");

            lock (Lock)
            {
                Databases.TryGetValue(_connectionString, out var database);

                if (database is not null)
                    return database;

                var client = new MongoClient(_connectionString);

                database = client.GetDatabase(_defaultDatabaseName);

                Databases[_connectionString] = database;

                return database;
            }
        }
    }
}
