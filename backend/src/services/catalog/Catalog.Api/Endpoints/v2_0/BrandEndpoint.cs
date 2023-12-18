using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints.V2_0;

public static class BrandEndpoint
{

    public static IEndpointRouteBuilder AddBrandRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("brands").WithTags("Brands");


        group.MapGet("/", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllBrandsQuery());
            return Results.Ok(result);
        });

        
        return app;
    }
}