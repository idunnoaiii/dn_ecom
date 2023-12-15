using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetProductByIdQuery(string Id): IRequest<ProductResponse>;
