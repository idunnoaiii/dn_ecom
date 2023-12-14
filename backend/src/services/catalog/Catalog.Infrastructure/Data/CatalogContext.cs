using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{

    public IMongoCollection<Product> Products { get; }

    public IMongoCollection<ProductType> Types { get; }

    public IMongoCollection<ProductBrand> Brands { get; }


    // NOTE:[thien.nguyen] is there better way to do this
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        Brands = database.GetCollection<ProductBrand>(nameof(Brands));
        Types = database.GetCollection<ProductType>(nameof(Types));
        Products = database.GetCollection<Product>(nameof(Products));

        // NOTE:[thien.nguyen] why dont move this to bootstrapting phase
        BrandContextSeed.SeedData(Brands);
        TypeContextSeed.SeedData(Types);
        ProductContextSeed.SeedData(Products);
    }
}
