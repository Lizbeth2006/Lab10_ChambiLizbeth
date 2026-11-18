using Lab10_ChambiLizbeth.Application.Ports;
using Lab10_ChambiLizbeth.Domain.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Adapters.Repositories;
using Lab10_ChambiLizbeth.Infrastructure.Adapters.Services;
using Lab10_ChambiLizbeth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_ChambiLizbeth.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketeraDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException(
                                       "Falta ConnectionStrings:DefaultConnection en appsettings.");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}