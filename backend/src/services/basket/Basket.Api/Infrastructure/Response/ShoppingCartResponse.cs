namespace Basket.Api.Response;

public class ShoppingCartResponse
{
    public string? Username { get; set; }

    public List<ShoppingCartItemResonse>? Items { get; set; }


    public decimal TotalPrice
    {
        get
        {
            return Items?.Aggregate(0M, (acc, item) => acc + item.Quantity * item.Price) ?? 0M;
        }
    }
}
