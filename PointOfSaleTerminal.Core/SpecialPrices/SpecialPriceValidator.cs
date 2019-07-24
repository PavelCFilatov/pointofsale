using FluentValidation;

namespace PointOfSale.Core.SpecialPrices
{
    public class SpecialPriceValidator : AbstractValidator<SpecialPrice>
    {
        public SpecialPriceValidator()
        {
            RuleFor(x => x.SpecialVolume)
                .GreaterThan((uint)1)
                .WithMessage("Special volume must be greater than 1");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Special price must be greater than 0");
        }
    }
}
