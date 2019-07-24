using PointOfSale.Core.Items;

namespace PointOfSale.Core.SpecialPrices
{
    public class SpecialPrice
    {
        public SpecialPrice(Item item, uint specialVolume, decimal price)
        {
            Item = item;
            SpecialVolume = specialVolume;
            Price = price;
        }

        public Item Item { get; }

        public uint SpecialVolume { get; }

        public decimal Price { get; }
    }
}
