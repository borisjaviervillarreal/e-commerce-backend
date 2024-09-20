using OrderService.Application.Services;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Clients;
using OrderService.Infrastructure.Persistence.Repositories;
using OrderService.Infrastructure.Persistence.UnitOfWork;

namespace OrderService.Infrastructure.DependencyInjection
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Inyección de AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inyección de repositorios y Unit of Work
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Inyección de servicios
            services.AddScoped<IPedidoService, PedidoService>();
            //services.AddScoped<IInventarioClient, InventarioClient>();

            // Registrar ProductoClient como HttpClient
            services.AddHttpClient<IInventarioClient, InventarioClient>();

            return services;
        }
    }
}
