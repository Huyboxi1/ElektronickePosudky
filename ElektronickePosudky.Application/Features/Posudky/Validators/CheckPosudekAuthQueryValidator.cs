using FluentValidation;
using ElektronickePosudky.Application.Features.Posudky.Queries;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class CheckPosudekAuthQueryValidator : AbstractValidator<CheckPosudekAuthQuery>
{
    public CheckPosudekAuthQueryValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .SetValidator(new PosudekAuthCheckDtoValidator());
    }
}
