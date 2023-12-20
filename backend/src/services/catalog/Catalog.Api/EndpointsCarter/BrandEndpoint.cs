using Carter;

namespace Catalog.Api.EndpointsCarter;

public class BrandEndpoint : CarterModule
{

    public BrandEndpoint()
        : base("/api/v1/brands")
    {
        WithTags("Brands");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {

        // app.MapGet("/", async ([FromServices] IMediator mediator) =>
        // {
        //     var result = await mediator.Send(new GetAllBrandsQuery());
        //     return Results.Ok(result);
        // });
    }
}