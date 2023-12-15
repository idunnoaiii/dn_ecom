
using Carter;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints;

public class ProductEndpoint : CarterModule
{

    public ProductEndpoint()
        : base("/api/v1/products")
    {
        WithTags("Products");
        // WithGroupName("product group");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (string id, [FromServices] IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        app.MapGet("/get-by-name/{productName}", async (string productName, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByNameQuery(productName));
            return Results.Ok(result);
        });

        app.MapGet("/", async ([FromServices] IMediator mediator) =>
        {

            var result = await mediator.Send(new GetAllProductsQuery());
            return Results.Ok(result);
        });

        app.MapGet("/get-by-brand/{brand}", async (string brand, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByBrandNameQuery(brand));
            return Results.Ok(result);
        });


        app.MapPost("/", async ([FromBody] CreateProductCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        app.MapPut("/", async ([FromBody] UpdateProductCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        app.MapDelete("/{id}", async (string id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteProductByIdCommand(id));
            return Results.Ok(result);
        });
    }

    private Task CreateProductAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private async Task<Results<Ok<string>, BadRequest<string>, ProblemHttpResult>> GetAllProductsAsync(HttpContext context)
    {
        return TypedResults.Ok("ok");
    }
}
