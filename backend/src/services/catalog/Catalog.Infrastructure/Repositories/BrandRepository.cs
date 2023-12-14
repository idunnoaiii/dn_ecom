using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    public Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        throw new NotImplementedException();
    }
}
