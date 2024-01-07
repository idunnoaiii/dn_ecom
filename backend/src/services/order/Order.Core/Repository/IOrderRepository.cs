namespace Order.Core.Repository;

using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entity;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserName(string userName);
}
