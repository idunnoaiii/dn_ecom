using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Query;

namespace Order.Api.Endpoint.V1_0.GetByUsername;

public static class GetByUsernameEndpoint
{
    public static IEndpointRouteBuilder AddGetByUsername(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("by/username/{username}", async (
            string username,
            [FromServices] IMediator mediator
        ) =>
        {
            var query = new GetOrderListQuery { Username = username };
            var orders = await mediator.Send(query);
            return Results.Ok(orders);
        });

        return builder;
    }
}
