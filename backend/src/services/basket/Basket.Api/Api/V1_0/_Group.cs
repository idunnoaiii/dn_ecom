using Basket.Api.Api.V1_0.Basket;

namespace Basket.Api.Api.V1_0;

public static class _Group
{
    public static RouteGroupBuilder GroupVersion1(this RouteGroupBuilder group)
    {
        var subGroup = group.MapGroup("baskets").WithTags("Baskets");

        subGroup
            .GetBasketByUser()
            .UpdateBasket()
            .DeleteBasket();


        return subGroup;
    }
}
