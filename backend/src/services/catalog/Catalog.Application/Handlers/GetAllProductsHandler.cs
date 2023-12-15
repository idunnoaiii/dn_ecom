using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllProductsHandler(
    IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts();
        var productsResponse = LazyMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(products.ToList());
        return productsResponse;
    }
}
