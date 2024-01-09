using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Command;

namespace Order.Api.Endpoint.V1_0.DeleteOrder;


public static class DeleteOrderEndpoint
{
    public static IEndpointRouteBuilder AddDeleteOrder(this IEndpointRouteBuilder builder)
    {

        builder.MapDelete("{id}", async (
            [FromServices] IMediator mediator,
            [FromRoute] int id
        )
        =>
        {
            var cmd = new DeleteOrderCommand { Id = id };
            var result = await mediator.Send(cmd);
            return Results.NoContent();
        });

        return builder;

    }
}
