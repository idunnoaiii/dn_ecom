using Basket.Api.Endpoint;
using Basket.Api.Application.GrpcService;
using Basket.Api.Application.Repository;
using Basket.Api.Extension;
using Basket.Api.Repository;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Neith.Core.Infras.OpenApi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddMapping();
    builder.AddApi();

    builder.Services.AddHealthChecks()
        .AddRedis(builder.Configuration["DatabaseSettings:ConnectionString"]!, "Redis heathcheck", HealthStatus.Degraded);

    builder.Services.AddStackExchangeRedisCache(option =>
    {
        option.Configuration = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
    });

    builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
        o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!)
    );

    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    builder.Services.AddScoped<DiscountGrpcService>();
}


var app = builder.Build();
{
    // app.UseHttpsRedirection();
    app.UseApi();
    app.MapEndpoints();
}

app.Run();