using System.Reflection;
using Discount.Api.Services;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using Discount.Infrastructure.Configurations;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;
using MediatR;
using Neith.Core.Infras.Mapper;
using Neith.Core.Infras.OpenApi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddMapster();
    //builder.AddApi();
    builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection(nameof(DatabaseSetting)));
    builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
    builder.Services.AddMediatR(typeof(CreateDiscountCommand).GetTypeInfo().Assembly);
    builder.Services.AddGrpc();
}

var app = builder.Build();
{

    app.MigrateDatabase();

    //app.UseApi();
    app.MapGrpcService<DiscountService>();
    app.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoint must be made through a gRPC client.");
    });
}


app.Run();
