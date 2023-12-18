using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;


using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.Api.ServiceRegistration.Api;

public class SwaggerConfigurationsOptions : IConfigureOptions<SwaggerGenOptions>
{

    public readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public SwaggerConfigurationsOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {

        foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateApiInfo(description));
        }

    }

    private static OpenApiInfo CreateApiInfo(ApiVersionDescription description)
        => new()
        {
            Title = "NeithShop Catalog APIs",
            Version = description.ApiVersion.ToString()
        };


}
