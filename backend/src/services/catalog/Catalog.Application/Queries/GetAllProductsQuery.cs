using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetAllProductsQuery(CatalogSpecParam CatalogSpecParam) : IRequest<Pagination<ProductResponse>>
{
}
