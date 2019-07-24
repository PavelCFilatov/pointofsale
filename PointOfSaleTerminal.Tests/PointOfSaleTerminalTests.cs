using System.Collections.Generic;
using NUnit.Framework;
using PointOfSale.Core;
using PointOfSale.Core.Items;
using PointOfSale.Core.SpecialPrices;

namespace PointOfSale.Tests
{
    [TestFixture]
    public class PointOfSaleTerminalTests
    {
        private PointOfSaleTerminal _terminal;

        [SetUp]
        public void SetUp()
        {
            var productA = new Item("A", 1.25m);
            var productC = new Item("C", 1);

            var items = new List<Item>
            {
                productA,
                new Item("B", 4.25m),
                productC,
                new Item("D", 0.75m),
            };

            var specialPrices = new List<SpecialPrice>
            {
                new SpecialPrice(productA, 3, 3),
                new SpecialPrice(productC, 6, 5),
            };

            var itemManager = new ItemManager(new ItemValidator());
            var specialPricesManager = new SpecialPricesManager(itemManager, new SpecialPriceValidator());
            var basket = new Basket(itemManager, specialPricesManager);
            _terminal = new PointOfSaleTerminal(itemManager, specialPricesManager, basket);
            _terminal.SetPricing(items, specialPrices);
        }

        [Test]
        [TestCase("ABCDABA", 13.25)]
        [TestCase("CCCCCCC", 6)]
        [TestCase("ABCD", 7.25)]
        [TestCase("", 0)]
        [TestCase("ABAACADABA", 16.25)]
        public void Test(string itemList, decimal expectedTotal)
        {
            foreach(var itemName in itemList.ToCharArray())
            {
                _terminal.Scan(itemName.ToString());
            }

            Assert.AreEqual(expectedTotal, _terminal.CalculateTotal());
        }
    }
}
