namespace Basket.Api.Application.Entity;

public class ShoppingCart
{
    public string? Username { get; set; }
    public List<ShoppingCartItem>? Items { get; set; }
}
