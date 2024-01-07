using Microsoft.Extensions.Logging;
using OrderEntity = Order.Core.Entity.Order;

namespace Order.Infrastructure.Data;


public class OrderContextSeed
{

    public static async Task Seed(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (context.Orders.Any() is false)
        {
            await context.Orders.AddRangeAsync(GetOrders());
            await context.SaveChangesAsync();
            logger.LogInformation("Order Database Seeded");
        }
    }

    private static OrderEntity[] GetOrders() => [
            new OrderEntity
            {
                UserName = "neith",
                FirstName = "neith",
                Lastname = "Q. Nguyen",
                EmailAddress = "neith@gmail.com",
                AddressLine = "HCM",
                Country = "Vietnam",
                TotalPrice = 4000,
                State = "HCM",
                ZipCode = "51000",

                CardName = "visa",
                CardNumber = "123456789",
                Expiration = "01/30",
                Cvv = "123",
                PaymentMethod = 1,
            }
        ];
}
