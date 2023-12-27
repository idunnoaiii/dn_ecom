using Discount.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        IServiceProvider service = scope.ServiceProvider;
        var config = service.GetRequiredService<IConfiguration>();
        var dbSettingOption = service.GetRequiredService<IOptions<DatabaseSetting>>();
        var logFactory = service.GetRequiredService<ILoggerFactory>();
        var logger = logFactory.CreateLogger(nameof(MigrateDatabase));

        try
        {
            logger.LogInformation("DB migration started");

            RunMigrate(dbSettingOption.Value);


            logger.LogInformation("DB migration completed");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Fail to migrate database");
        }

        return host;

    }

    private static void RunMigrate(DatabaseSetting value)
    {
        using var con = new NpgsqlConnection(value.ConnectionString);
        con.Open();

        using var command = new NpgsqlCommand
        {
            Connection = con
        };
        
        command.CommandText = """
            DROP TABLE IF EXISTS Coupons;
            CREATE TABLE Coupons (
                Id SERIAL PRIMARY KEY,
                ProductName VARCHAR(500) NOT NULL,
                Description Text,
                Amount numeric
            );
        """;

        command.ExecuteNonQuery();

        command.CommandText = """
            INSERT INTO Coupons (ProductName, Description, Amount) VALUES ('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);
            INSERT INTO Coupons (ProductName, Description, Amount) VALUES ('Yonex vCore Pro 100 A Tenis Racquet (270gm, Strung)', 'Racquet Discount', 500);
        """;

        command.ExecuteNonQuery();

    }
}
