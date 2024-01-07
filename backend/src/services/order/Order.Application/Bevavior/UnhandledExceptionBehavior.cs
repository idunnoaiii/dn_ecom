using MediatR;
using Microsoft.Extensions.Logging;


namespace Order.Application.Bevavior;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger) 
: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (System.Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Unhandled exception occurred with Request {RequestName}", requestName);
            
            throw;
        }
    }
}
