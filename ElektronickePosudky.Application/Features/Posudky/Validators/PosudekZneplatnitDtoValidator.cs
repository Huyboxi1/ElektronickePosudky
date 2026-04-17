using FluentValidation;
using ElektronickePosudky.Application.DTOs;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class PosudekZneplatnitDtoValidator : AbstractValidator<PosudekZneplatnitDto>
{
    public PosudekZneplatnitDtoValidator()
    {
        RuleFor(x => x.KrzpId)
            .NotEmpty().WithMessage("KrzpIdRequired")
            .MaximumLength(50).WithMessage("KrzpIdTooLong");

        RuleFor(x => x.Ico)
            .NotEmpty().WithMessage("IcoRequired")
            .MaximumLength(20).WithMessage("IcoTooLong");

        RuleFor(x => x.DuvodZneplatneni)
            .NotNull()
            .SetValidator(new CodebookItemDtoValidator());
    }
}
