using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Data;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Order.Core.Common;
using Order.Core.Entity;

public class OrderContext : DbContext
{

    public DbSet<Order> Orders { get; set; }

    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "test";// TODO: fix hardcode here
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "test";// TODO: fix hardcode here
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;

                default: break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


}
