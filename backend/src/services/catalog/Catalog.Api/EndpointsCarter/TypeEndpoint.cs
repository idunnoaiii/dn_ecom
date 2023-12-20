using Carter;

namespace Catalog.Api.EndpointsCarter;

public class TypeEndpoint : CarterModule
{

    public TypeEndpoint()
        : base("/api/v1/types")
    {
        WithTags("Types");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {

        // app.MapGet("/", async ([FromServices] IMediator mediator) =>
        // {
        //     var result = await mediator.Send(new GetAllTypesQuery());
        //     return Results.Ok(result);
        // });
    }
}