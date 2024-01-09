using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Command;

namespace Order.Api.Endpoint.V1_0.UpdateOrder;

public static class UpdateOrderEndpoint
{
    public static IEndpointRouteBuilder AddUpdateOrder(this IEndpointRouteBuilder builder)
    {

        builder.MapPut("", async (
            [FromServices] IMediator mediator,
            [FromBody] UpdateOrderCommand command
        )
        =>
        {
            var result = await mediator.Send(command);
            return Results.NoContent();
        });

        return builder;
    
    }
}
