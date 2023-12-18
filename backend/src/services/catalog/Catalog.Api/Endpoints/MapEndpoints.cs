using Catalog.Api.Endpoints.V1_0;
using Catalog.Api.Endpoints.V2_0;
using Catalog.Api.ServiceRegistration.Api;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Catalog.Api.Endpoints;

public static class MapEndpointsEx
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/v1").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(1.0).GroupVersion1();
        app.MapGroup("/api/v2").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(2.0).GroupVersion2();

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });


        return app;
    }
}
