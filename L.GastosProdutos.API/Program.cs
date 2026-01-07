using L.GastosProdutos.API.IOC;
using Microsoft.AspNetCore.Mvc;
using L.GastosProdutos.Core.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            var dbPath = Path.Combine(dataDir, "gastos.db");
            ConfigureBindings.Sqlite(builder.Services, dbPath);
            // MediatR removed; using simple application services
            ConfigureBindings.ConfigureCors(builder.Services);
            ConfigureBindings.Services(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;
                var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(basePath, apiXml);
                if (File.Exists(apiXmlPath))
                {
                    options.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
                }

                var coreAssembly = typeof(EnumUnitOfMeasure).Assembly;
                var coreXml = $"{coreAssembly.GetName().Name}.xml";
                var coreXmlPath = Path.Combine(basePath, coreXml);
                if (File.Exists(coreXmlPath))
                {
                    options.IncludeXmlComments(coreXmlPath, includeControllerXmlComments: false);
                }
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<L.GastosProdutos.Core.Infra.Sqlite.AppDbContext>();
                    var hasMigrations = db.Database.GetMigrations().Any();
                    if (hasMigrations)
                    {
                        db.Database.Migrate();
                        app.Logger.LogInformation("Database migration applied successfully.");
                    }
                    else
                    {
                        db.Database.EnsureCreated();
                        app.Logger.LogWarning("No EF Core migrations found. Ensured database created from model.");
                    }
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Error initializing database.");
                    throw;
                }
            }
        
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            // Map application exceptions to HTTP status codes with ProblemDetails
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (NotFoundException ex)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Not Found",
                        Detail = ex.Message
                    };
                    await context.Response.WriteAsJsonAsync(problem);
                }
                catch (InvalidOperationException ex)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Bad Request",
                        Detail = ex.Message
                    };
                    await context.Response.WriteAsJsonAsync(problem);
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "An unhandled exception occurred.");
                    
                    if (app.Environment.IsDevelopment())
                    {
                        throw; // Let developer see the full stack trace
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Internal Server Error",
                        Detail = "An unexpected error occurred."
                    };
                    await context.Response.WriteAsJsonAsync(problem);
                }
            });

            app.UseCors(ConfigureBindings.CORS_POLICY);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
