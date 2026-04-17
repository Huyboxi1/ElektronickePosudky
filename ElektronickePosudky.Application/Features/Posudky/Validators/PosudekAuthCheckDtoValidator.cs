using FluentValidation;
using ElektronickePosudky.Application.DTOs;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class PosudekAuthCheckDtoValidator : AbstractValidator<PosudekAuthCheckDto>
{
    public PosudekAuthCheckDtoValidator()
    {
        RuleFor(x => x.KrzpId)
            .NotEmpty().WithMessage("KrzpIdRequired")
            .MaximumLength(50).WithMessage("KrzpIdTooLong");

        RuleFor(x => x.Ico)
            .NotEmpty().WithMessage("IcoRequired")
            .MaximumLength(20).WithMessage("IcoTooLong");
    }
}
