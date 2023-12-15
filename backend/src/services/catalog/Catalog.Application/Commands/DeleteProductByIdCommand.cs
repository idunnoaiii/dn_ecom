using MediatR;

namespace Catalog.Application.Commands;

public record class DeleteProductByIdCommand(string Id) : IRequest<bool>;
