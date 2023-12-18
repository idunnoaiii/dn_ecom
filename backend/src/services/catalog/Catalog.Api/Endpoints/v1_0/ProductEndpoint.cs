
using Carter;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Core.Specs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Catalog.Api.Model.GetAllProduct;

namespace Catalog.Api.Endpoints.V1_0;

public static class ProductEndpoint
{

    public static IEndpointRouteBuilder AddProductRoutes(this IEndpointRouteBuilder app)
    {

        var group = app.MapGroup("products").WithTags("Products");

        group.MapGet("/{id}", async (string id, [FromServices] IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        group.MapGet("/by/name/{productName}", async (string productName, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByNameQuery(productName));
            return Results.Ok(result);
        });

        group.MapGet("/", async ([AsParameters] GetAllProductRequest request, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllProductsQuery(request.Adapt<CatalogSpecParam>()));
            return Results.Ok(result);
        })
        .WithOpenApi(op => new (op) 
        {
            Summary = "Search products"
        });

        group.MapGet("/by/brand/{brand}", async (string brand, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByBrandNameQuery(brand));
            return Results.Ok(result);
        });

        group.MapPost("/", async ([FromBody] CreateProductCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPut("/", async ([FromBody] UpdateProductCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapDelete("/{id}", async (string id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteProductByIdCommand(id));
            return Results.Ok(result);
        });

        return app;
    }

    // private Task CreateProductAsync(HttpContext context)
    // {
    //     throw new NotImplementedException();
    // }

    // private async Task<Results<Ok<string>, BadRequest<string>, ProblemHttpResult>> GetAllProductsAsync(HttpContext context)
    // {
    //     return TypedResults.Ok("ok");
    // }
}
