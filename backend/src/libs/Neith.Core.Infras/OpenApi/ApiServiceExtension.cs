using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Neith.Core.Infras.OpenApi;

public static class ApiServiceExtension

{
    public static IHostApplicationBuilder AddApi(this IHostApplicationBuilder builder)
    {
        
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV"; // Formats the version as follow: "'v'major[.minor]"
        });

        // Open Api
        builder.Services.AddSwaggerGen(options => {
            options.EnableAnnotations();
            // options.ResolveConflictingActions(option => option.First());//TODO.[thien.nguyen] // workaround
        });


        builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationsOptions>();

        //services.AddCarter();
        //
        // builder.Services.AddHealthChecks();

        return builder;
    }


    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapApiVersionSet();

        // var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app
        .UseSwagger()
        .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "Version 1.0");
                c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", "Version 2.0");

                // foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                // {
                //     c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
                // }
            });
        

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }

    public static ApiVersionSet ApiVersionSet { get; private set; } = default!;

    public static ApiVersionSet MapApiVersionSet(this IEndpointRouteBuilder builder)
    {
        ApiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(1.0)
            .HasApiVersion(2.0)
            .Build();

        return ApiVersionSet;
    }
}

