using ElektronickePosudky.Application.DTOs;
using FluentValidation;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class CodebookItemDtoValidator : AbstractValidator<CodebookItemDto>
{
    public CodebookItemDtoValidator()
    {
        RuleFor(x => x.Kod)
            .NotEmpty()
            .WithMessage("CodebookKodRequired");

        RuleFor(x => x.Verze)
            .NotEmpty()
            .WithMessage("CodebookVerzeRequired");
    }
}

public class HarmonizovanyKodDetailDtoValidator : AbstractValidator<HarmonizovanyKodDetailDto>
{
    public HarmonizovanyKodDetailDtoValidator()
    {
        RuleFor(x => x.HarmonizovanyKod)
            .NotNull()
            .WithMessage("HarmonizovanyKodRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleForEach(x => x.SkupinaRo)
            .SetValidator(new CodebookItemDtoValidator());
    }
}

public class NarodniKodDetailDtoValidator : AbstractValidator<NarodniKodDetailDto>
{
    public NarodniKodDetailDtoValidator()
    {
        RuleFor(x => x.NarodniKod)
            .NotNull()
            .WithMessage("NarodniKodRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.SkupinaRo)
            .NotNull()
            .WithMessage("SkupinaRoRequired")
            .SetValidator(new CodebookItemDtoValidator());
    }
}

public class PosudekSkupinaRoDetailDtoValidator : AbstractValidator<PosudekSkupinaRoDetailDto>
{
    public PosudekSkupinaRoDetailDtoValidator()
    {
        RuleFor(x => x.SkupinaRo)
            .NotNull()
            .WithMessage("SkupinaRoRequired")
            .SetValidator(new CodebookItemDtoValidator());
    }
}

public class PosudekZpusobilostDtoValidator : AbstractValidator<PosudekZpusobilostDto>
{
    public PosudekZpusobilostDtoValidator()
    {
        RuleFor(x => x.SkupinaZadateleRidic)
            .NotNull()
            .WithMessage("SkupinaZadateleRidicRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.SkupinyRidicskehoOpravneni)
            .NotEmpty()
            .WithMessage("SkupinyRidicskehoOpravneniRequired");

        RuleForEach(x => x.SkupinyRidicskehoOpravneni)
            .SetValidator(new PosudekSkupinaRoDetailDtoValidator());

        RuleFor(x => x.Vysledek)
            .NotNull()
            .WithMessage("VysledekRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleForEach(x => x.HarmonizovaneKody)
            .SetValidator(new HarmonizovanyKodDetailDtoValidator());

        RuleForEach(x => x.NarodniKody)
            .SetValidator(new NarodniKodDetailDtoValidator());
    }
}

public class PosudekRoCreateDtoValidator : AbstractValidator<PosudekRoCreateDto>
{
    public PosudekRoCreateDtoValidator()
    {
        RuleFor(x => x.Rid)
            .NotEmpty()
            .Length(10)
            .WithMessage("RidInvalidLength");

        RuleFor(x => x.KrzpId)
            .NotEmpty()
            .WithMessage("KrzpIdRequired");

        RuleFor(x => x.TypAkce)
            .NotNull()
            .WithMessage("TypAkceRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.StavPosudku)
            .NotNull()
            .WithMessage("StavPosudkuRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.DruhProhlidky)
            .NotNull()
            .WithMessage("DruhProhlidkyRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.DruhPosudku)
            .NotNull()
            .WithMessage("DruhPosudkuRequired")
            .SetValidator(new CodebookItemDtoValidator());

        RuleFor(x => x.DatumVystaveni)
            .NotEmpty()
            .WithMessage("DatumVystaveniRequired")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("DatumVystaveniFuture");

        RuleFor(x => x.PlatnostDo)
            .GreaterThanOrEqualTo(x => x.DatumVystaveni)
            .WithMessage("PlatnostDoInvalid")
            .When(x => x.PlatnostDo.HasValue);

        RuleFor(x => x.Zpusobilosti)
            .NotEmpty()
            .WithMessage("ZpusobilostiRequired");

        RuleForEach(x => x.Zpusobilosti)
            .SetValidator(new PosudekZpusobilostDtoValidator());
    }
}
