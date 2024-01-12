using FluentValidation;

namespace Application.Features.Url.Commands.GenerateUrl;

public sealed class GenerateUrlCommandValidator : AbstractValidator<GenerateUrlCommand>
{
    public GenerateUrlCommandValidator()
    {
        RuleFor(q => q.OriginalUrl)
            .NotEmpty()
            .WithMessage("Url cannot be null or empty");
    }
}