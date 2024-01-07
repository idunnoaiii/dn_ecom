namespace Order.Application.Exception;

public class OrderNotFoundException : ApplicationException
{
    public OrderNotFoundException(object id) : base($"Order {id} is not found")
    {
        
    }
}
