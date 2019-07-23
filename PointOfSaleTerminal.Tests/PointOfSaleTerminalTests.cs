using System.Collections.Generic;
using NUnit.Framework;
using PointOfSale.Core;

namespace PointOfSale.Tests
{
    [TestFixture]
    public class PointOfSaleTerminalTests
    {
        private PointOfSaleTerminal _terminal;

        [SetUp]
        public void SetUp()
        {
            var item = new List<Item>
            {
                new Item("A", 1.25m, 3, 3),
                new Item("B", 4.25m),
                new Item("C", 1, 6, 5),
                new Item("D", 0.75m),
            };

            _terminal = new PointOfSaleTerminal();
            _terminal.SetPricing(item);
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
