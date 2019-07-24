using FluentValidation;

namespace PointOfSale.Core
{
    /// <summary>
    /// It is not required by task and current code.
    /// But it would be required for real cases so wrong values could not be assigned to Item
    /// </summary>
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty()
                .WithMessage("Name must not be empty");

            RuleFor(x => x.ItemPrice)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
}
