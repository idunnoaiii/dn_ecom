using Basket.Api.Application.Entity;

namespace Basket.Api.Application.Repository;


public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasket(string username);
    Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart);
    Task DeleteBasket(string username);
}