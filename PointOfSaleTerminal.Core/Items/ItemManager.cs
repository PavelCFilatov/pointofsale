using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Core.Items
{
    public interface IItemManager
    {
        (bool Success, string ValidationMessage) AddItem(Item item);

        Item GetItem(string itemName);

        bool ItemExists(string itemName);
    }

    public class ItemManager : IItemManager
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly IValidator<Item> _itemValidator;

        public ItemManager(IValidator<Item> itemValidator)
        {
            _itemValidator = itemValidator ?? throw new ArgumentNullException("itemValidator is null");
        }

        public (bool Success, string ValidationMessage) AddItem(Item item)
        {
            if (_items.Any(x => string.Equals(x.ItemName, item.ItemName, StringComparison.OrdinalIgnoreCase)))
            {
                return (false, $"Item with name {item.ItemName} already exists in list.");
            }

            var validationResult = _itemValidator.Validate(item);

            if (validationResult.IsValid)
            {
                _items.Add(item);

                return (true, string.Empty);
            }

            return (false, $"Item: {item.ItemName} {string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage))}");
        }

        public Item GetItem(string itemName)
        {
            return _items.SingleOrDefault(x => string.Equals(x.ItemName, itemName, StringComparison.OrdinalIgnoreCase));
        }

        public bool ItemExists(string itemName)
        {
            return _items.Any(x => string.Equals(x.ItemName, itemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
