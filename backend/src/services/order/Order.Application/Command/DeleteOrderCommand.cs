using MediatR;

namespace Order.Application.Command;

public class DeleteOrderCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
