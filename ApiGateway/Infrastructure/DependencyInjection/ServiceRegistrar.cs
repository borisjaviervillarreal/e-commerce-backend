using ApiGateway.Application.Services;
using ApiGateway.Domain.Ports;
using ApiGateway.Infrastructure.Persistence.Repositories;
using ApiGateway.Infrastructure.Persistence.UnitOfWork;
using ApiGateway.Infrastructure.Security;

namespace ApiGateway.Infrastructure.DependencyInjection
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Inyección de AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inyección de JwtHandler
            services.AddScoped<IJwtHandler, JwtHandler>();

            // Inyección de repositorios y Unit of Work
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Inyección de servicios
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
