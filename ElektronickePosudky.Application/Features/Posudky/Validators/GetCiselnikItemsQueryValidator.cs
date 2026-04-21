using FluentValidation;
using ElektronickePosudky.Application.Features.Ciselniky.Queries;

namespace ElektronickePosudky.Application.Features.Posudky.Validators;

public class GetCiselnikItemsQueryValidator : AbstractValidator<GetCiselnikItemsQuery>
{
    public GetCiselnikItemsQueryValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Kod)
            .NotEmpty().WithMessage("CodebookKodRequired");
    }
}
