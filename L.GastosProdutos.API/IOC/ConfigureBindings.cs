using L.GastosProdutos.Core.Application.Implementations;
using L.GastosProdutos.Core.Application.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Settings;

namespace L.GastosProdutos.API.IOC
{
    public static class ConfigureBindings
    {
        public static void Mongo(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("Mongo"));

            services.AddSingleton<IMongoConnections, MongoConnections>();
            services.AddSingleton<IMongoContext, MongoContext>();

            ConfigureMongoRepositories(services);
        }

        private static void ConfigureMongoRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IPackingRepository, PackingRepository>();
        }
    }
}
