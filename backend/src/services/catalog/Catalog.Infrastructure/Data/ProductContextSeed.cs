using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class ProductContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProduct = productCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "products.json");

        if (checkProduct)
        {
            return;
        }

        var productsData = File.ReadAllText(path);
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        if (products?.Count > 0)
        {
            foreach (var item in products)
            {
                productCollection.InsertOne(item);
            }
        }
    }
}
