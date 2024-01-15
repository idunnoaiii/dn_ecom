using Mapster;
using Neith.Core.Infras.Mapper;
using Neith.Core.Infras.OpenApi;
using Neith.Core.Infras.EventBus;
using Order.Api.Endpoint;
using Order.Api.Extension;
using Order.Application.Extension;
using Order.Infrastructure.Data;
using Order.Infrastructure.Extension;
using Neith.Core.EventBus.Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddMapster();
    builder.AddApi();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddEventBus(builder.Configuration, (ctx, cfg) => {
        // cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c=> {
        //     c.ConfigureConsumer
        // });
    });
    builder.Services.AddApplication();
    builder.Services.AddHealthChecks().Services.AddDbContext<OrderContext>();
}

var app = builder.Build();
{
    //app.UseAuthorization();
    // app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    // {
    //     Predicate = _ => true,
    //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    // });
    
    app.MigrateDatabase<OrderContext>((ctx, svcs) => 
    {
        var logger = svcs.GetRequiredService<ILogger<OrderContextSeed>>();
        OrderContextSeed.Seed(ctx, logger).Wait();
    });
    
    app.UseApi();
    app.MapEndpoints();

    
}



app.Run();

