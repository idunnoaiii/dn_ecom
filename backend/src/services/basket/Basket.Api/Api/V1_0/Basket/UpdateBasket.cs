using Basket.Api.Application.Entity;
using Basket.Api.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Api.V1_0.Basket;

public static class UpdateBasketExt
{
    public static IEndpointRouteBuilder UpdateBasket(this IEndpointRouteBuilder builder) 
    {
        builder.MapPost("/{username}", async (
            string username, 
            [FromBody] ShoppingCart payload,
            [FromServices] IBasketRepository basketRepository) => 
        {
            var result = await basketRepository.UpdateBasket(payload);
            return Results.Ok(result);
        });
        return builder;
    }
}

public class CreateBasketRequest 
{
    
}
