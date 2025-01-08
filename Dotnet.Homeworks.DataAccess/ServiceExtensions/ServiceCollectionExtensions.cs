using System.Reflection;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.DataAccess.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IProductRepository, IProductRepository>();
    }
}