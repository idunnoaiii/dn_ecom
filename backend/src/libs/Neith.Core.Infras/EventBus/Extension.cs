using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Neith.Core.Infras.EventBus;

public static class Extension
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configEndpointAction = null) 
    {
        services.AddMassTransit(config => {
            
            config.UsingRabbitMq((ctx, cfg) => {
                
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                
                configEndpointAction?.Invoke(ctx, cfg); 

            });
            

        });
        
        return services;
    }
}
