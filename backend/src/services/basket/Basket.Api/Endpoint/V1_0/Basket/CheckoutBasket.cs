using Microsoft.AspNetCore.Mvc;
using Basket.Api.Application.Entity;
using MapsterMapper;
using Basket.Api.Application.Repository;
using Neith.Core.EventBus.Event;
using MassTransit;

namespace Basket.Api.Endpoint.V1_0.Basket;



public static class CheckoutBasket
{
    public static IEndpointRouteBuilder AddCheckoutBasket(this IEndpointRouteBuilder builder) 
    {
        builder.MapPost("checkout", 
        async (
            [FromBody] BasketCheckout payload,
            [FromServices] IMapper mapper,
            [FromServices] IBasketRepository repository,
            [FromServices] IPublishEndpoint publisher
        ) => {
            var basket = await repository.GetBasket(payload.Username!);
            
            if (basket is null)
                return Results.BadRequest();
                
            var eventMsg = mapper.Map<BasketCheckoutEvent>(basket);

            eventMsg.TotalPrice = basket.Items.Aggregate(0M, (acc, item) => acc += item.Price);

            await publisher.Publish(eventMsg);            
            
            await repository.DeleteBasket(payload.Username!);
            
            return Results.Accepted();
        });

        return builder;

    }
}
