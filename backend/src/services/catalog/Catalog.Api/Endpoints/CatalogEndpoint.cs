
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints;

public static class CatalogGroupEndpoint
{
    public static void AddCatalogRouterGroup(this IEndpointRouteBuilder builder)
    {

        var groupp = builder.MapGroup("/v1/api/catalogs").WithTags("Catalogs");

        groupp.MapGet("", GetAllProductsAsync).WithDisplayName("Get all products");
        groupp.MapPost("", CreateProductAsync);
        
    }

    private static async Task CreateProductAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<Results<Ok<string>, BadRequest<string>, ProblemHttpResult>> GetAllProductsAsync(HttpContext context)
    {
        return TypedResults.Ok("ok");
    }
}
