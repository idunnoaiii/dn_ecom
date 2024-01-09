using FluentValidation;
using Order.Application.Command;

namespace Order.Application.Validator;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("{Username} is required") 
            .NotNull()
            .WithMessage("{Username} is required") ;

        RuleFor(x => x.TotalPrice) 
            .NotEmpty()
            .WithMessage("Totalprice is required")
            .GreaterThan(-1)
            .WithMessage("TotalPrice should not be negative");
        
        // ...
    }
}
