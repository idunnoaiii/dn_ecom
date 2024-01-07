using FluentValidation.Results;

namespace Order.Application.Exception;

public class ValidationException : ApplicationException
{

    public Dictionary<string, string[]> Errors { get; set; } = [];

    public ValidationException() : base("One or more validation errors orcurred")
    {

    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failures => failures.Key, failures => failures.ToArray());
    }
}
