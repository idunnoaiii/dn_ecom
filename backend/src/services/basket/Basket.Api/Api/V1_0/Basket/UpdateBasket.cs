using Basket.Api.Application.Entity;
using Basket.Api.Application.GrpcService;
using Basket.Api.Application.Repository;
using Basket.Api.Response;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Api.V1_0.Basket;

public static class UpdateBasketExt
{
    public static IEndpointRouteBuilder UpdateBasket(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/{username}", async (
            string username,
            [FromBody] ShoppingCart payload,
            [FromServices] IBasketRepository basketRepository,
            [FromServices] DiscountGrpcService discountGrpcSvc,
            IMapper mapper) =>
        {

            foreach (var item in payload.Items) 
            {
                var coupon = await discountGrpcSvc.GetDiscount(item.ProductName!);
                item.Price -= new decimal(coupon.Amount);
            }

            var result = await basketRepository.UpdateBasket(payload);

            var response = mapper.Map<ShoppingCartResponse>(result!);

            return Results.Ok(response);

        });

        return builder;
    }
}

public class CreateBasketRequest
{

}
