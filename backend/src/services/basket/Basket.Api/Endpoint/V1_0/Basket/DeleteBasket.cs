using Basket.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Endpoint.V1_0.Basket;

public static class DeleteBasket
{

    public static IEndpointRouteBuilder AddDeleteBasket(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/{username}",
        async (
            string username,
            [FromServices] BasketRepository basketRepository
        ) =>
        {
            await basketRepository.DeleteBasket(username);
            return Results.Ok();
        });
        return builder;
    }
}
