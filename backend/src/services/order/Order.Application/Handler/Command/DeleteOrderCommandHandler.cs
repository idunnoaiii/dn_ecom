using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Command;
using Order.Application.Exception;
using Order.Core.Repository;

namespace Order.Application.Handler.Command;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{

    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }


    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderDelete = await _orderRepository.GetById(request.Id);


        if (orderDelete is null)
        {
            throw new OrderNotFoundException(request.Id);
        }

        await _orderRepository.Delete(orderDelete);
        _logger.LogInformation("Order {Id} deleted", request.Id);

        return Unit.Value;
    }
}
