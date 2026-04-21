using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class PosudekZneplatnitDtoValidator : AbstractValidator<PosudekZneplatnitDto>
{
    public PosudekZneplatnitDtoValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.KrzpId)
            .NotEmpty().WithMessage("KrzpIdRequired");

        RuleFor(x => x.Ico)
            .NotEmpty().WithMessage("IcoRequired");

        RuleFor(x => x.DuvodZneplatneniKod)
            .NotEmpty().WithMessage("CodebookKodRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("akce-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");
    }
}