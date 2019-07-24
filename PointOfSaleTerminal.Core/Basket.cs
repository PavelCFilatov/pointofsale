using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Core.Items;
using PointOfSale.Core.SpecialPrices;

namespace PointOfSale.Core
{
    public interface IBasket
    {
        bool Scan(string itemName);

        decimal CalculateTotal();
    }

    public class Basket : IBasket
    {
        private readonly IItemManager _itemManager;
        private readonly ISpecialPricesManager _specialPricesManager;
        private readonly Dictionary<string, uint> _basket = new Dictionary<string, uint>();

        public Basket(IItemManager itemManager, ISpecialPricesManager specialPricesManager)
        {
            _itemManager = itemManager ?? throw new ArgumentNullException("ItemManager is null");
            _specialPricesManager = specialPricesManager ?? throw new ArgumentNullException("SpecialPricesManager is null");
        }

        /// <summary>
        /// Adds item to basket.
        /// </summary>
        /// <param name="itemName">Name of price which must be added.</param>
        /// <returns>True if item was found in item list and added to basket, otherwise false.</returns>
        public bool Scan(string itemName)
        {
            if (_itemManager.ItemExists(itemName))
            {
                if (_basket.ContainsKey(itemName))
                {
                    _basket[itemName] += 1;
                }
                else
                {
                    _basket.Add(itemName, 1);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculates total value of items that are in the basket considering special prices.
        /// </summary>
        /// <returns>Total value of items.</returns>
        public decimal CalculateTotal()
        {
            return _basket
                .Select(x => calculateItemTotal(x.Key, x.Value))
                .Sum();
        }

        private decimal calculateItemTotal(string itemName, uint itemQuantity)
        {
            var itemSpecialPrices = _specialPricesManager.GetSpecialPrice(itemName);
            var item = _itemManager.GetItem(itemName);

            if (itemSpecialPrices == null)
            {
                return itemQuantity * item.ItemPrice;
            }
            else
            {
                var specialVolumes = itemQuantity / itemSpecialPrices.SpecialVolume;
                decimal total = specialVolumes * itemSpecialPrices.Price;

                var remainedItems = itemQuantity - (specialVolumes * itemSpecialPrices.SpecialVolume);
                total += remainedItems * item.ItemPrice;

                return total;
            }
        }
    }
}
