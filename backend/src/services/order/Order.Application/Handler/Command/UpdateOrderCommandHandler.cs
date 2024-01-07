using MapsterMapper;
using MediatR;
using Order.Application.Command;
using Order.Core.Repository;
using Microsoft.Extensions.Logging;
using Order.Application.Exception;

namespace Order.Application.Handler.Command;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderUpdate = await _orderRepository.GetById(request.Id);
        

        if (orderUpdate is null) 
        {
            throw new OrderNotFoundException(request.Id);
        }

        orderUpdate = _mapper.Map(request, orderUpdate);


        await _orderRepository.Update(orderUpdate);
        
        _logger.LogInformation("Order {Id} is successfully updated", request.Id);

        return Unit.Value;
    }
}
