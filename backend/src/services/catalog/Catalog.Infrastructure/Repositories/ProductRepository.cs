using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository, IBrandRepository, ITypeRepository
{

    private readonly ICatalogContext _context = context;

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
        DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _context.Brands.Find(x => true).ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context.Types.Find(x => true).ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Brands.Name, name);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<Pagination<Product>> GetProducts(CatalogSpecParam catalogSpecParam)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        if (catalogSpecParam.Search is { Length: > 0 } search)
        {
            filter &= builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(search));
        }

        if (catalogSpecParam.BrandId is { Length: > 0 } brandId)
        {
            filter &= builder.Eq(x => x.Brands.Id, brandId);
        }

        if (catalogSpecParam.TypeId is { Length: > 0 } typeId)
        {
            filter &= builder.Eq(x => x.Types.Id, typeId);
        }

        if (catalogSpecParam.Sort is { Length: > 0 } sort)
        {
            return new Pagination<Product>
            {
                PageSize = catalogSpecParam.PageSize,
                PageIndex = catalogSpecParam.PageIndex,
                Data = await DataFilter(catalogSpecParam, filter),
                Count = await _context.Products.CountDocumentsAsync(filter)
            };
        }

        return new Pagination<Product>
        {
            PageSize = catalogSpecParam.PageSize,
            PageIndex = catalogSpecParam.PageIndex,
            Data = await _context
                .Products
                .Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip(catalogSpecParam.PageSize * (catalogSpecParam.PageIndex - 1))
                .Limit(catalogSpecParam.PageSize)
                .ToListAsync(),
            Count = await _context.Products.CountDocumentsAsync(filter)
        };
    }

    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParam catalogSpecParam, FilterDefinition<Product> filter)
    {
        var sort = catalogSpecParam.Sort switch
        {
            "priceAsc" => Builders<Product>.Sort.Ascending(nameof(Product.Price)),
            "priceDesc" => Builders<Product>.Sort.Descending(nameof(Product.Price)),
            _ => Builders<Product>.Sort.Ascending(nameof(Product.Name))
        };

        return await _context
                .Products
                .Find(filter)
                .Sort(sort)
                .Skip(catalogSpecParam.PageSize * (catalogSpecParam.PageIndex - 1))
                .Limit(catalogSpecParam.PageSize)
                .ToListAsync();

    }
}
