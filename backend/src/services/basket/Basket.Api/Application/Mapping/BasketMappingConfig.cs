using Basket.Api.Application.Entity;
using Basket.Api.Response;
using Mapster;

namespace Basket.Api.Application.Mapping;

public class BasketMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCartItem, ShoppingCartItemResonse>();
        config.NewConfig<ShoppingCart, ShoppingCartResponse>()
            .Map(dest => dest.Items, src => src.Items.Select(x => x.Adapt<ShoppingCartItemResonse>()).ToList());
    }
}
