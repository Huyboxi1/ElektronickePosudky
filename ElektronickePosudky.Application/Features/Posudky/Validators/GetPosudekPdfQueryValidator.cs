using FluentValidation;
using ElektronickePosudky.Application.Features.Posudky.Queries;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class GetPosudekPdfQueryValidator : AbstractValidator<GetPosudekPdfQuery>
{
    public GetPosudekPdfQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("PosudekIdRequired");
    }
}
