using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Core.Repository;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;

namespace Order.Infrastructure.Extension;

public static class AddInfrastructureExt
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        services.AddScoped<IOrderRepository, OrderRepository>();  
        
        return services;
    }
}
