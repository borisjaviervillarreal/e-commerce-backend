using ProductService.Application.Services;
using ProductService.Domain.Ports;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Persistence.Repositories;
using ProductService.Infrastructure.Persistence.UnitOfWork;

namespace ProductService.Infrastructure.DependencyInjection
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Inyección de AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inyección de repositorios y Unit of Work
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Inyección de servicios
            services.AddScoped<IProductoService, ProductoService>();

            return services;
        }
    }
}
