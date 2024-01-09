using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Command;

namespace Order.Api.Endpoint.V1_0.CheckoutOrder;

public static class CheckoutOrderEndpoint
{
    public static IEndpointRouteBuilder AddCheckoutOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("", async (
            [FromServices] IMediator mediator,
            [FromBody] CheckoutOrderCommand command
        )
        =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        return builder;
    }
}
