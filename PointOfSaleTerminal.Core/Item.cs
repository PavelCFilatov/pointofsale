namespace PointOfSale.Core
{
    public class Item
    {
        public Item(string itemName, decimal itemPrice)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
        }

        public string ItemName { get; }

        public decimal ItemPrice { get; }

        public virtual bool HasSpecialVolumeDeal => false;
    }
}
