using MediatR;
using Order.Application.Response;

namespace Order.Application.Query;

public class GetOrderListQuery : IRequest<List<OrderResponse>>
{
    public string? UserName { get; set; }
}
