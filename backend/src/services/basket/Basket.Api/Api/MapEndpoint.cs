using Basket.Api.Api.V1_0;
using Neith.Core.Infras.OpenApi;

namespace Basket.Api.Api;

public static class MapEndpointEx

{

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/v1").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(1.0).GroupVersion1();
        //app.MapGroup("/api/v2").WithApiVersionSet(ApiServiceExtension.ApiVersionSet).MapToApiVersion(2.0).GroupVersion2();
        return app;
    }
}
