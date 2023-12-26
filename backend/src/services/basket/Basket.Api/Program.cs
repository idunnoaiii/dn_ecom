using Basket.Api.Api;
using Basket.Api.Application.Repository;
using Basket.Api.Extension;
using Basket.Api.Repository;
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

    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
}


var app = builder.Build();
{
    // app.UseHttpsRedirection();
    app.UseApi();
    app.MapEndpoints();
}

app.Run();