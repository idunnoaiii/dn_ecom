using Basket.Api.Application.Entity;
using Basket.Api.Application.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repository;


public class BasketRepository : IBasketRepository
{
    
    private readonly IDistributedCache _distributedCache;

    public BasketRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task DeleteBasket(string username)
    {
        await _distributedCache.RemoveAsync(username);
    }

    public async Task<ShoppingCart?> GetBasket(string username)
    {
        var item = await _distributedCache.GetStringAsync(username);
        
        if (item is null) 
            return null;
        
        return JsonConvert.DeserializeObject<ShoppingCart>(item);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _distributedCache.SetStringAsync(shoppingCart.Username, JsonConvert.SerializeObject(shoppingCart));
        return await GetBasket(shoppingCart.Username);
    }
}