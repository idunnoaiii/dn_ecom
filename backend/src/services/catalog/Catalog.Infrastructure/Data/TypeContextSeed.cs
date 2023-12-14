using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkType = typeCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "types.json");

        if (checkType)
        {
            return;
        }

        var brandsData = File.ReadAllText(path);
        var types = JsonSerializer.Deserialize<List<ProductType>>(brandsData);

        if (types?.Count > 0)
        {
            foreach (var item in types)
            {
                typeCollection.InsertOne(item);
            }
        }
    }
}
