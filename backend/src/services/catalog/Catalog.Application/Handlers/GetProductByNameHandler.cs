using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{

    private readonly IProductRepository _productRepository;

    public GetProductByNameHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductByName(request.Name);
        return LazyMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(products.ToList());
    }
}
