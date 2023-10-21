using ApiCorrelation.Configurations;
using ApiCorrelation.Configurations.Interfaces;

namespace ApiCorrelation.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorrelationIdManager(this IServiceCollection services)
    {
        services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        return services;
    }
}
