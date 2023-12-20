using Basket.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Api.V1_0.Basket;

public static class DeleteBasketExt
{

    public static IEndpointRouteBuilder DeleteBasket(this IEndpointRouteBuilder builder)
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
