using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Bevavior;
using Order.Application.Handler.Command;

namespace Order.Application.Extension;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {

        services.AddMediatR(typeof(CheckoutOrderCommandHandler).GetTypeInfo().Assembly);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        return services;
    }
}
