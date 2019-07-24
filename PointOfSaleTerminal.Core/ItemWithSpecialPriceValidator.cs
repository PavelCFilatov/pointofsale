using FluentValidation;

namespace PointOfSale.Core
{
    public class ItemWithSpecialPriceValidator : AbstractValidator<ItemWithSpecialPrice>
    {
        public ItemWithSpecialPriceValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty()
                .WithMessage("Name must not be empty");

            RuleFor(x => x.ItemPrice)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.SpecialVolume)
                .Equal((uint)1)
                .WithMessage("Special volum must be greater than 1");

            RuleFor(x => x.SpecialVolumePrice)
                .GreaterThan(0)
                .When(x => x.SpecialVolume > 1)
                .WithMessage("Special price must be greater than 0");
        }
    }
}
