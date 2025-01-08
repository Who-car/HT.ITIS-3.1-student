using System.Reflection;
using Dotnet.Homeworks.Mediator.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Mediator.DependencyInjectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] handlersAssemblies)
    {
        // Ключ - тип обрабатываемого запроса
        // Значение - обработчик
        Dictionary<Type, Type> handlerTypes = [];
        
        foreach (var handlerAssembly in handlersAssemblies)
            foreach (var handlerType in handlerAssembly.GetTypes())
                foreach (var handlerInterface in handlerType.GetInterfaces())
                {
                    if (!handlerInterface.IsGenericType) continue;
                    
                    var genericDefinition = handlerInterface.GetGenericTypeDefinition();
                    if (genericDefinition != typeof(IRequestHandler<>)
                        && genericDefinition != typeof(IRequestHandler<,>)) continue;
                    
                    services.AddScoped(handlerInterface, handlerType);
                    var genericArguments = handlerInterface.GetGenericArguments();
                    var requestType = genericArguments[0];
                        
                    // в текущей реализации на один тип запроса не может быть 
                    // больше одного обработчика
                    if (!handlerTypes.TryAdd(requestType, handlerType))
                    {
                        throw new InvalidOperationException($"Multiple handlers found for request type {requestType.FullName}");
                    }
                }

        services.AddSingleton<IMediator, Mediator>(provider =>
        {
            var mediator = new Mediator(provider);
            foreach (var handlerType in handlerTypes)
            {
                mediator.Register(handlerType.Key, handlerType.Value);
            }

            return mediator;
        });
        
        return services;
    }
}