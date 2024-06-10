using L.GastosProdutos.Core;
using L.GastosProdutos.Core.Application.Repository;
using L.GastosProdutos.Core.Infra.Mongo.Interfaces;
using L.GastosProdutos.Core.Infra.Mongo.Settings;
using L.GastosProdutos.Core.Interfaces;

namespace L.GastosProdutos.API.IOC
{
    public static class ConfigureBindings
    {
        public const string CORS_POLICY = "AllowAll";

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

        public static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY,
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        private static void ConfigureMongoRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IPackingRepository, PackingRepository>();
        }
    }
}
