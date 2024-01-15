using Basket.Api.Endpoint;
using Basket.Api.Application.GrpcService;
using Basket.Api.Application.Repository;
using Basket.Api.Extension;
using Basket.Api.Repository;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Neith.Core.Infras.OpenApi;
using Neith.Core.Infras.EventBus;

var builder = WebApplication.CreateBuilder(args);
{
    
    var configuration = builder.Configuration;

    builder.AddMapping();
    builder.AddApi();

    builder.Services.AddHealthChecks()
        .AddRedis(configuration["DatabaseSettings:ConnectionString"]!, "Redis heathcheck", HealthStatus.Degraded);

    builder.Services.AddStackExchangeRedisCache(option =>
    {
        option.Configuration = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
    });

    builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
        o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!)
    );

    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    builder.Services.AddScoped<DiscountGrpcService>();
    
    builder.Services.AddEventBus(configuration);
}


var app = builder.Build();
{
    // app.UseHttpsRedirection();
    app.UseApi();
    app.MapEndpoints();
}

app.Run();