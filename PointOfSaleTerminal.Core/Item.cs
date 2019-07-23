namespace PointOfSale.Core
{
    public class Item
    {
        public Item(string itemName, decimal itemPrice) : this(itemName, itemPrice, 0, 0.0m)
        {
        }

        public Item(string itemName, decimal itemPrice, uint specialVolume, decimal specialVolumePrice)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
            SpecialVolume = specialVolume;
            SpecialVolumePrice = specialVolumePrice;
        }

        public string ItemName { get; }

        public decimal ItemPrice { get; }

        public uint SpecialVolume { get; }

        public decimal SpecialVolumePrice { get; }

        public bool HasSpecialVolumeDeal => SpecialVolume > 1 && SpecialVolumePrice > 0;
    }
}
