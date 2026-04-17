using FluentValidation;
using ElektronickePosudky.Application.Features.Posudky.Commands;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class InvalidatePosudekCommandValidator : AbstractValidator<InvalidatePosudekCommand>
{
    public InvalidatePosudekCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("PosudekIdRequired");

        RuleFor(x => x.IfMatch)
            .NotEmpty().WithMessage("IfMatchRequired");

        RuleFor(x => x.Data)
            .NotNull()
            .SetValidator(new PosudekZneplatnitDtoValidator());
    }
}
