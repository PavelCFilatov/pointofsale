using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Core.Items;
using PointOfSale.Core.SpecialPrices;

namespace PointOfSale.Core
{
    public class PointOfSaleTerminal
    {
        private readonly IItemManager _itemManager;
        private readonly ISpecialPricesManager _specialPricesManager;
        private readonly IBasket _basket;

        public PointOfSaleTerminal(
            IItemManager itemManager,
            ISpecialPricesManager specialPricesManager,
            IBasket basket)
        {
            _itemManager = itemManager ?? throw new ArgumentNullException("ItemManager is null");
            _specialPricesManager = specialPricesManager ?? throw new ArgumentNullException("SpecialPricesManager is null");
            _basket = basket ?? throw new ArgumentNullException("Basket is null");
        }

        /// <summary>
        /// Sets list of available items and available special prices.
        /// </summary>
        /// <param name="items">List of available items.</param>
        /// <param name="specialPrices">List of special prices.</param>
        /// <returns>List of validation errors for passed items and special prices.</returns>
        public List<string> SetPricing(IEnumerable<Item> items, IEnumerable<SpecialPrice> specialPrices)
        {
            
            var itemsValidationErrors = items.Select(_itemManager.AddItem)
                .Where(x => !x.Success)
                .Select(x => x.ValidationMessage);

            var specialPricesValidationMessages = specialPrices.Select(_specialPricesManager.AddSpecialPrice)
                .Where(x => !x.Success)
                .Select(x => x.ValidationMessage);

            return itemsValidationErrors.Union(specialPricesValidationMessages).ToList();
        }

        public void Scan(string itemName)
        {
            _basket.Scan(itemName);
        }

        public decimal CalculateTotal()
        {
            return _basket.CalculateTotal();
        }
    }
}
