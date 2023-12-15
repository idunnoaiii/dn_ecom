using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Application.Handlers;

public class GetProductByBrandNameHandler
{
     private readonly IProductRepository _productRepository;

    public GetProductByBrandNameHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductByName(request.BrandName);
        return LazyMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(products.ToList());
    }
}
