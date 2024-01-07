using MediatR;

namespace Order.Application.Command;

public class CheckoutOrderCommand : IRequest<int>
{
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }


    public string? FirstName { get; set; }
    public string? Lastname { get; set; }

    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }


    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }

    public int? PaymentMethod { get; set; }
}