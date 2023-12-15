
using Carter;
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

    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (string id, [FromServices] IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        app.MapPost("", () => Results.Ok());
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
