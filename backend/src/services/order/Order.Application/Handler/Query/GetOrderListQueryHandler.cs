using MapsterMapper;
using MediatR;
using Order.Application.Query;
using Order.Application.Response;
using Order.Core.Repository;

namespace Order.Application.Handler.Query;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetByUserName(request.UserName!);
        return _mapper.Map<List<OrderResponse>>(orderList);
    }
}
