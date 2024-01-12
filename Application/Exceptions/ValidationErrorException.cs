using FluentValidation.Results;

namespace Application.Exceptions;

public sealed class ValidationErrorException : Exception
{
    public List<string> Errors { get; set; }
    
    public ValidationErrorException(string message) : base(message)
    {

    }
    
    public ValidationErrorException(string message, IEnumerable<ValidationFailure> errors) : base(message)
    {
        var errorMessages = errors.Select(error => error.ErrorMessage).ToList();

        Errors = errorMessages;
    }
}