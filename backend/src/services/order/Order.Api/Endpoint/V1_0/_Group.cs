using Order.Api.Endpoint.V1_0.CheckoutOrder;
using Order.Api.Endpoint.V1_0.DeleteOrder;
using Order.Api.Endpoint.V1_0.GetByUsername;
using Order.Api.Endpoint.V1_0.UpdateOrder;

namespace Order.Api.Endpoint.V1_0;

public static class _Group
{
    public static RouteGroupBuilder GroupVersion1(this RouteGroupBuilder group)
    {
        var subGroup = group.MapGroup("orders").WithTags("Orders");

        subGroup
            .AddGetByUsername()
            .AddCheckoutOrder()
            .AddUpdateOrder()
            .AddDeleteOrder()
            ;

        return subGroup;
    }
}
