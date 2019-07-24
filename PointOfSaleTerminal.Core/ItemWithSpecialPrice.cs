namespace PointOfSale.Core
{
    public class ItemWithSpecialPrice : Item
    {
        public ItemWithSpecialPrice(string itemName, decimal itemPrice, uint specialVolume, decimal specialVolumePrice)
            : base(itemName, itemPrice)
        {
            SpecialVolume = specialVolume;
            SpecialVolumePrice = specialVolumePrice;
        }

        public uint SpecialVolume { get; }

        public decimal SpecialVolumePrice { get; }

        public override bool HasSpecialVolumeDeal => true;
    }
}
