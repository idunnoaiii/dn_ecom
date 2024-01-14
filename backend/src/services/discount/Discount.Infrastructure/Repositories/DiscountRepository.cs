using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly DatabaseSetting _databaseSetting;

    public DiscountRepository(IOptions<DatabaseSetting> databaseSettingOption)
    {
        _databaseSetting = databaseSettingOption.Value;
    }

    #region Queries
    public async Task<Coupon> GetDiscount(string productName)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(_databaseSetting.ConnectionString);

        Coupon? coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("""
            SELECT * FROM Coupons WHERE ProductName = @productName
        """, new { Productname = productName });

        if (coupon is null)
        {
            return new Coupon
            {
                ProductName = "No discount",
                Amount = 0,
                Description = "No discount available"
            };
        }

        return coupon;

    }

    #endregion Queries

    #region Commands
    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using NpgsqlConnection con = new(_databaseSetting.ConnectionString);

        var affected = await con.ExecuteAsync("""
            INSERT INTO Coupons (ProductName, Amount, Description) VALUES (@ProductName, @Amount, @Description)
        """,
        coupon);

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using NpgsqlConnection con = new(_databaseSetting.ConnectionString);
        var affected = await con.ExecuteAsync("""
            DELETE Coupons WHERE ProductName = @ProductName
        """,
        new {
            ProductName = productName
        });
        
        return affected > 0;
    }


    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using NpgsqlConnection con = new(_databaseSetting.ConnectionString);

        var affected = await con.ExecuteAsync("""
            UPDATE Coupons 
            SET ProductName = @ProductName, Amount = @Amount, Description = @Description)
            WHERE Id = @Id
        """,
        coupon);

        return affected > 0;
    }

    #endregion Commands
}
