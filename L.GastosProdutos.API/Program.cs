using L.GastosProdutos.API.IOC;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using L.GastosProdutos.Core.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
            Directory.CreateDirectory(dataDir);
            var dbPath = Path.Combine(dataDir, "gastos.db");
            ConfigureBindings.Sqlite(builder.Services, dbPath);
            // MediatR removed; using simple application services
            ConfigureBindings.ConfigureCors(builder.Services);
            ConfigureBindings.Services(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<L.GastosProdutos.Core.Infra.Sqlite.AppDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            // Map application NotFoundException to HTTP 404 with ProblemDetails
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
            });

            app.UseCors(ConfigureBindings.CORS_POLICY);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
