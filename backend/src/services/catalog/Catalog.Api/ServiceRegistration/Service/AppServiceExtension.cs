using System.Reflection;
using Catalog.Application.Handlers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using MediatR;

namespace Catalog.Api.ServiceRegistration.Service;

public static class AppServiceExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);

        
        
        // others service
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, ProductRepository>();
        services.AddScoped<ITypeRepository, ProductRepository>();




        return services;
    }
}
