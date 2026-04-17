using ElektronickePosudky.Application.Features.Posudky.Commands;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class CreatePosudekCommandValidator : AbstractValidator<CreatePosudekCommand>
{
    public CreatePosudekCommandValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .SetValidator(new PosudekRoCreateDtoValidator());
    }
}
