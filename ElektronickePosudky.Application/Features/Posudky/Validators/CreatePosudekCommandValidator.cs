using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Repositories;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class CreatePosudekCommandValidator : AbstractValidator<CreatePosudekCommand>
{
    public CreatePosudekCommandValidator(ICiselnikRepository ciselnikRepository)
    {
        RuleFor(x => x.Data)
            .NotNull().WithMessage("data is required.")
            .SetValidator(new PosudekRoCreateDtoValidator(ciselnikRepository));
    }
}