using FluentValidation;

namespace Application.Features.Url.Queries.GetUrlById;

public sealed class GetUrlByIdQueryValidator : AbstractValidator<GetUrlByIdQuery>
{
    public GetUrlByIdQueryValidator()
    {
        RuleFor(q => q.UrlId)
            .Must(ValidateUrlId)
            .WithMessage("Id should be Guid");
    }
    
    private bool ValidateUrlId(string urlId)
    {
        return Guid.TryParse(urlId, out _);
    }
}