using Basket.Api.Application.Repository;

namespace Basket.Api.Api.V1_0.Basket;

public static class GetBasketByUserExt
{
    public static IEndpointRouteBuilder GetBasketByUser(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("by/username/{username}",
        async (
            string username,
            IBasketRepository basketRepository
        ) =>
        {
            var basket = await basketRepository.GetBasket(username);
            return Results.Ok(basket);
        });

        return builder;
    }
}