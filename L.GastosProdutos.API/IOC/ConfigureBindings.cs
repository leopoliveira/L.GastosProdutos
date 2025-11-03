using L.GastosProdutos.Core;
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

            // Repositories removed; services use DbContext directly.
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

        // Repository registrations removed
    }
}
