using Carter;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints.V1_0;

public static class TypeEndpoint
{

    public static IEndpointRouteBuilder AddTypeRoutes(this IEndpointRouteBuilder app)
    {
        
        var group = app.MapGroup("types").WithTags("Types");

        group.MapGet("/", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllTypesQuery());
            return Results.Ok(result);
        });
        

        return app;
    }
}