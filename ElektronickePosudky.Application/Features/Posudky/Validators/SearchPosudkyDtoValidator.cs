using ElektronickePosudky.Application.DTOs;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class SearchPosudkyDtoValidator : AbstractValidator<SearchPosudkyDto>
{
    public SearchPosudkyDtoValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageInvalid");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100)
            .WithMessage("SizeInvalid");

        RuleFor(x => x.Rid)
            .MaximumLength(50)
            .WithMessage("RidTooLong")
            .When(x => !string.IsNullOrEmpty(x.Rid));

        RuleFor(x => x.Ico)
            .MaximumLength(8)
            .WithMessage("IcoInvalid")
            .When(x => !string.IsNullOrEmpty(x.Ico));

        RuleFor(x => x.DatumDo)
            .GreaterThanOrEqualTo(x => x.DatumOd)
            .WithMessage("DatumDoInvalid")
            .When(x => x.DatumOd.HasValue && x.DatumDo.HasValue);
    }
}