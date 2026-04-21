using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class HarmonizovanyKodCreateDtoValidator : AbstractValidator<HarmonizovanyKodCreateDto>
{
    public HarmonizovanyKodCreateDtoValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.HarmonizovanyKod)
            .NotEmpty().WithMessage("HarmonizovanyKodRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("seznam-harmonizovane-kody-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleForEach(x => x.SkupinaRoKody)
            .NotEmpty().WithMessage("SkupinaRoRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("seznam-skupin-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");
    }
}

public class NarodniKodCreateDtoValidator : AbstractValidator<NarodniKodCreateDto>
{
    public NarodniKodCreateDtoValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.NarodniKod)
            .NotEmpty().WithMessage("NarodniKodRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("seznam-narodni-kody-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.SkupinaRoKod)
            .NotEmpty().WithMessage("SkupinaRoRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("seznam-skupin-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");
    }
}

public class PosudekZpusobilostCreateDtoValidator : AbstractValidator<PosudekZpusobilostCreateDto>
{
    public PosudekZpusobilostCreateDtoValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.SkupinaZadateleRidicKod)
            .NotEmpty().WithMessage("SkupinaZadateleRidicRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("skupina-zadatel-ridic-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.SkupinyRidicskehoOpravneniKody)
            .NotEmpty().WithMessage("SkupinyRidicskehoOpravneniRequired");

        RuleForEach(x => x.SkupinyRidicskehoOpravneniKody)
            .NotEmpty().WithMessage("SkupinaRoRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("seznam-skupin-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.VysledekKod)
            .NotEmpty().WithMessage("VysledekRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("vysledek-posudku-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleForEach(x => x.HarmonizovaneKody)
            .SetValidator(new HarmonizovanyKodCreateDtoValidator(repository));

        RuleForEach(x => x.NarodniKody)
            .SetValidator(new NarodniKodCreateDtoValidator(repository));
    }
}

public class PosudekRoCreateDtoValidator : AbstractValidator<PosudekRoCreateDto>
{
    public PosudekRoCreateDtoValidator(ICiselnikRepository repository)
    {
        RuleFor(x => x.Rid)
            .NotEmpty().WithMessage("RidInvalidLength")
            .Length(10).WithMessage("RidInvalidLength");

        RuleFor(x => x.KrzpId)
            .NotEmpty().WithMessage("KrzpIdRequired");

        RuleFor(x => x.TypAkceKod)
            .NotEmpty().WithMessage("TypAkceRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("akce-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.StavPosudkuKod)
            .NotEmpty().WithMessage("StavPosudkuRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("stav-posudku", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.DruhProhlidkyKod)
            .NotEmpty().WithMessage("DruhProhlidkyRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("druh-prohlidky-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.DruhPosudkuKod)
            .NotEmpty().WithMessage("DruhPosudkuRequired")
            .MustAsync((kod, ct) => repository.PolozkaExistsAsync("druh-posudku-ro", kod, ct))
            .WithMessage("InvalidCodebookValue");

        RuleFor(x => x.DatumVystaveni)
            .NotEmpty().WithMessage("DatumVystaveniRequired")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DatumVystaveniFuture");

        RuleFor(x => x.PlatnostDo)
            .GreaterThanOrEqualTo(x => x.DatumVystaveni).WithMessage("PlatnostDoInvalid")
            .When(x => x.PlatnostDo.HasValue);

        RuleFor(x => x.Zpusobilosti)
            .NotEmpty().WithMessage("ZpusobilostiRequired");

        RuleForEach(x => x.Zpusobilosti)
            .SetValidator(new PosudekZpusobilostCreateDtoValidator(repository));
    }
}