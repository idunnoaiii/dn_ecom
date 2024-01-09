using Microsoft.EntityFrameworkCore;
using Order.Core.Repository;
using Order.Infrastructure.Data;
using OrderEntity = Order.Core.Entity.Order;

namespace Order.Infrastructure.Repository;

public class OrderRepository(OrderContext context) : RepositoryBase<OrderEntity>(context), IOrderRepository
{
    public async Task<IEnumerable<OrderEntity>> GetByUsername(string username)
    {
        var orderList = await _context.Orders
            .Where(x => x.Username == username)
            .ToListAsync();
        
        return orderList;
    }
}
