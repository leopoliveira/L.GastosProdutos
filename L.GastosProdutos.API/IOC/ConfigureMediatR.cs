using System.Reflection;

namespace L.GastosProdutos.API.IOC
{
    public static class ConfigureMediatR
    {
        public static void MediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
