using L.GastosProdutos.Core;
using L.GastosProdutos.Core.Application.Repository;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Interfaces;
using L.GastosProdutos.Core.Infrasctucture.Mongo.Settings;
using L.GastosProdutos.Core.Interfaces;

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

        public static void MediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(new AssemblyReference().GetAssembly()));
        }

        private static void ConfigureMongoRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IPackingRepository, PackingRepository>();
        }
    }
}
