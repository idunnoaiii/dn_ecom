using Basket.Api.Application.Repository;
using Basket.Api.Response;
using Mapster;
using MapsterMapper;

namespace Basket.Api.Endpoint.V1_0.Basket;

public static class GetBasketByUser
{
    public static IEndpointRouteBuilder AddGetBasketByUser(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("by/username/{username}",
        async (
            string username,
            IBasketRepository basketRepository,
            IMapper mapper
        ) =>
        {
            var basket = await basketRepository.GetBasket(username);

            if (basket is null)
            {
                return Results.NotFound();
            }

            var basketResponse = mapper.Map<ShoppingCartResponse>(basket);
            return Results.Ok(basketResponse);
        });

        return builder;
    }
}