using Microsoft.Extensions.Diagnostics.HealthChecks;
using Catalog.Api.ServiceRegistration.Api;
using Catalog.Api.ServiceRegistration.Service;


using static Catalog.Api.ServiceRegistration.Api.ApiServiceExtension;
using Catalog.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddApi(configuration);

// move add infrastucture
builder.Services
    .AddHealthChecks()
    .AddMongoDb(configuration["DatabaseSettings:ConnectionString"]!, "Catalog Mongo Db Health Check", HealthStatus.Degraded);

builder.Services.AddApplicationService(configuration);

var app = builder.Build();

app.UseApi(configuration);

app.MapEndpoints();

// app.MapGet("/version", () => "Hello version 1").WithApiVersionSet(ApiVersionSet).MapToApiVersion(1.0);
// app.MapGet("/version", () => "Hello version 2").WithApiVersionSet(ApiVersionSet).MapToApiVersion(2.0);
// app.MapGet("/version2only", () => "Hello version 2 only").WithApiVersionSet(ApiVersionSet).MapToApiVersion(2.0);
// app.MapGet("/versionneutral", () => "Hello neutral version")
//     .WithApiVersionSet(ApiVersionSet)
//     .IsApiVersionNeutral()
//     .WithOpenApi(operation => new(operation)
//     {
//         Summary = "Neutral",
//         Description = "This endpint is neutral"
//     });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
//app.UseAuthorization();



app.Run();
