using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Repositories;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class InvalidatePosudekCommandValidator : AbstractValidator<InvalidatePosudekCommand>
{
    public InvalidatePosudekCommandValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("PosudekIdRequired");

        RuleFor(x => x.IfMatch)
            .NotEmpty().WithMessage("IfMatchRequired");

        RuleFor(x => x.Data)
            .NotNull()
            .SetValidator(new PosudekZneplatnitDtoValidator(repository));
    }
}