using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetProductByBrandNameQuery (string BrandName) : IRequest<IList<ProductResponse>>;
