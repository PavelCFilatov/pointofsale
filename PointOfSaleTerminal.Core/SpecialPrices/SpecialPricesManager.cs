using FluentValidation;
using PointOfSale.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Core.SpecialPrices
{
    public interface ISpecialPricesManager
    {
        (bool Success, string ValidationMessage) AddSpecialPrice(SpecialPrice specialPrice);

        SpecialPrice GetSpecialPrice(string itemName);
    }

    public class SpecialPricesManager : ISpecialPricesManager
    {
        private readonly List<SpecialPrice> _specialPrices = new List<SpecialPrice>();
        private readonly IValidator<SpecialPrice> _specialPriceValidator;
        private readonly IItemManager _itemManager;

        public SpecialPricesManager(IItemManager itemManager, IValidator<SpecialPrice> specialPriceValidaor)
        {
            _specialPriceValidator = specialPriceValidaor ?? throw new ArgumentNullException("ItemValidator is null");
            _itemManager = itemManager ?? throw new ArgumentNullException("ItemManager is null");
        }
        public (bool Success, string ValidationMessage) AddSpecialPrice(SpecialPrice specialPrice)
        {
            if (!_itemManager.ItemExists(specialPrice.Item.ItemName))
            {
                return (false, $"Item {specialPrice.Item.ItemName} does not exist.");
            }

            if (_specialPrices.Any(x => string.Equals(x.Item.ItemName, specialPrice.Item.ItemName, StringComparison.OrdinalIgnoreCase)))
            {
                return (false, $"Special price for {specialPrice.Item.ItemName} already exists in list.");
            }

            var validationResult = _specialPriceValidator.Validate(specialPrice);

            if (validationResult.IsValid)
            {
                _specialPrices.Add(specialPrice);

                return (true, string.Empty);
            }

            return (false, $"Special price: {specialPrice.Item.ItemName} {string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage))}");
        }

        public SpecialPrice GetSpecialPrice(string itemName)
        {
            return _specialPrices.SingleOrDefault(x => string.Equals(x.Item.ItemName, itemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
