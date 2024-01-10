using Basket.Api.Endpoint.V1_0.Basket;

namespace Basket.Api.Endpoint.V1_0;

public static class Group
{
    public static RouteGroupBuilder AddGroupVersion1(this RouteGroupBuilder group)
    {
        var subGroup = group.MapGroup("baskets").WithTags("Baskets");

        subGroup
            .AddGetBasketByUser()
            .AddUpdateBasket()
            .AddDeleteBasket()
            .AddCheckoutBasket()
            ;


        return subGroup;
    }
}
