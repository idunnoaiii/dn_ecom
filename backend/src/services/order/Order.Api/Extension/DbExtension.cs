using Microsoft.EntityFrameworkCore;

namespace Order.Api.Extension;

public static class DbExtension
{

    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();

        TContext context = services.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Start DB migration");
            CallSeeder(seeder, context, services);
            logger.LogInformation("DB migration completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating DB");
        }

        return host;
    }

    private static void CallSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services)
    where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}
