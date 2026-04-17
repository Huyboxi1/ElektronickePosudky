using FluentValidation;
using ElektronickePosudky.Application.Features.Posudky.Queries;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class SearchPosudkyQueryValidator : AbstractValidator<SearchPosudkyQuery>
{
    public SearchPosudkyQueryValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .SetValidator(new SearchPosudkyDtoValidator());
    }
}
