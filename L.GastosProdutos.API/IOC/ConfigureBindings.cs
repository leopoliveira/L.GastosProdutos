using L.GastosProdutos.Core;
using L.GastosProdutos.Core.Application.Repository;
using L.GastosProdutos.Core.Interfaces;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Application.Services;
using L.GastosProdutos.Core.Application.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.API.IOC
{
    public static class ConfigureBindings
    {
        public const string CORS_POLICY = "AllowAll";

        public static void Sqlite(IServiceCollection services, string databasePath)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={databasePath}"));

            ConfigureRepositories(services);
        }

        // Application service registrations (replacing MediatR handlers)
        public static void Services(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IPackingService, PackingService>();
        }

        public static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IPackingRepository, PackingRepository>();
        }
    }
}
