using ElektronickePosudky.Application.Features.Posudky.Queries;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class GetPosudekHistoryQueryValidator : AbstractValidator<GetPosudekHistoryQuery>
{
    public GetPosudekHistoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("PosudekIdRequired");
    }
}
