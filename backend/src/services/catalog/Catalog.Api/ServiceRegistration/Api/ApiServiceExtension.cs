using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.Api.ServiceRegistration.Api;

public static class ApiServiceExtension

{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddApiVersioning(options =>
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
        services.AddSwaggerGen(options => {
            options.EnableAnnotations();
            // options.ResolveConflictingActions(option => option.First());//TODO.[thien.nguyen] // workaround
        });


        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationsOptions>();

        //services.AddCarter();

        return services;
    }


    public static WebApplication UseApi(this WebApplication app, IConfiguration configuration)
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

