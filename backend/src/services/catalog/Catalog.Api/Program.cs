using Carter;
using Catalog.Api.Endpoints;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MediatR;
using Catalog.Application.Handlers;
using System.Reflection;
using Catalog.Infrastructure.Data;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.WebUtilities;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var configuring = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(x =>
// {
//     x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "Catalog.Api",
//         Version = "v1"
//     });
// });
builder.Services.AddCarter();

builder.Services.AddApiVersioning()
.AddHealthChecks()
.AddMongoDb(configuring["DatabaseSettings:ConnectionString"]!, "Catalog Mongo Db Health Check", HealthStatus.Degraded);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
//app.UseAuthorization();


app.MapCarter();
app.MapHealthChecks("/health", new HealthCheckOptions {
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

