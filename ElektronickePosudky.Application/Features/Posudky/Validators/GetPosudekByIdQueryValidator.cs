using FluentValidation;
using ElektronickePosudky.Application.Features.Posudky.Queries;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class GetPosudekByIdQueryValidator : AbstractValidator<GetPosudekByIdQuery>
{
    public GetPosudekByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("PosudekIdRequired");
    }
}
