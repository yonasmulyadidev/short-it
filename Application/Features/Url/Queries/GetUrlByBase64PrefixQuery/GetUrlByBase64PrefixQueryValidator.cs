using FluentValidation;

namespace Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;

public sealed class GetUrlByBase64PrefixQueryValidator : AbstractValidator<GetUrlByBase64PrefixQuery>
{
    public GetUrlByBase64PrefixQueryValidator()
    {
        RuleFor(q => q.Base64Prefix)
            .NotEmpty()
            .WithMessage("Base64 prefix cannot be null or empty");
    }
}