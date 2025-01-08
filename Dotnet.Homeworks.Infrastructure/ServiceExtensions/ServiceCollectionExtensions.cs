using Dotnet.Homeworks.Infrastructure.UnitOfWork;

namespace Dotnet.Homeworks.Infrastructure.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}