using Basket.Api.Endpoint.V1_0;
using Neith.Core.Infras.OpenApi;

namespace Basket.Api.Endpoint;

public static class MapEndpointEx

{

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/v1").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(1.0).AddGroupVersion1();
        //app.MapGroup("/api/v2").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(2.0).GroupVersion2();
        return app;
    }
}
