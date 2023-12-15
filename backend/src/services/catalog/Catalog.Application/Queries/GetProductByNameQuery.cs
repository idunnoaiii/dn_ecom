using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetProductByNameQuery(string Name): IRequest<IList<ProductResponse>>;
