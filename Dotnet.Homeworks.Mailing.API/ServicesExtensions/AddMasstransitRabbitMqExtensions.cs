using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Helpers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        return services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(AssemblyReference.Assembly);
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitConfiguration.Hostname, h =>
                {
                    h.Username(rabbitConfiguration.Username);
                    h.Password(rabbitConfiguration.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}