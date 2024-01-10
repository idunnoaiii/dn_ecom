using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Neith.Core.Infras.EventBus;

public static class Extension
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddMassTransit(config => {
            config.UsingRabbitMq((ctx, cfg) => {
                cfg.Host(configuration["EventBusSetting:HostAddress"]);
            });
        });
        
        
        return services;
    }
}
