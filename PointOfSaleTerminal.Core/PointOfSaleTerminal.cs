using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Core
{
    public class PointOfSaleTerminal
    {
        private readonly List<Item> _items = new List<Item>();

        private readonly Dictionary<string, uint> _basket = new Dictionary<string, uint>();

        public void SetPricing(IEnumerable<Item> items)
        {
            _items.AddRange(items);
        }

        public bool Scan(string itemName)
        {
            if (_items.Any(x => x.ItemName == itemName))
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

        public decimal CalculateTotal()
        {
            return _basket
                .Select(x => calculateItemTotal(_items.Single(i => i.ItemName == x.Key), x.Value))
                .Sum();
        }

        private static decimal calculateItemTotal(Item item, uint itemQuantity)
        {
            if (item.HasSpecialVolumeDeal)
            {
                var itemWithSpecialPrice = item as ItemWithSpecialPrice;
                var specialVolumes = itemQuantity / itemWithSpecialPrice.SpecialVolume;
                decimal total = specialVolumes * itemWithSpecialPrice.SpecialVolumePrice;

                var remainedItems = itemQuantity - (specialVolumes * itemWithSpecialPrice.SpecialVolume);
                total += remainedItems * item.ItemPrice;

                return total;
            }
            else
            {
                return itemQuantity * item.ItemPrice;
            }
        }
    }
}
