using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Command;
using OrderEntity = Order.Core.Entity.Order;
using Order.Core.Repository;

namespace Order.Application.Handler.Command;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<OrderEntity>(request);
        var generateOrder = await _orderRepository.Add(orderEntity);
        _logger.LogInformation("Order {Id} successfully created", generateOrder.Id);
        return generateOrder.Id;
    }
}
