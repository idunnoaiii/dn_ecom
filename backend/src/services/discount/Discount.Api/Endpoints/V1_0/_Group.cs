
namespace Discount.Api.Endpoints.V1_0;

public static class _Group
{
    public static RouteGroupBuilder GroupVersion1(this RouteGroupBuilder group)
    {
        var subGroup = group.MapGroup("discounts").WithTags("Discounts");

        // subGroup
        //     .GetBasketByUser()
        //     .UpdateBasket()
        //     .DeleteBasket();


        return subGroup;
    }
}
