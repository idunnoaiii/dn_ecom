using Neith.Core.Infras.OpenApi;
using Order.Api.Endpoint.V1_0;

namespace Order.Api.Endpoint;

public static class MapEndpoint
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.Map("/", () => Results.Redirect("/swagger"));
        app.MapGroup("/api/v1").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(1.0).GroupVersion1();
        //app.MapGroup("/api/v2").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(2.0).GroupVersion2();
        return app;
    }
}
