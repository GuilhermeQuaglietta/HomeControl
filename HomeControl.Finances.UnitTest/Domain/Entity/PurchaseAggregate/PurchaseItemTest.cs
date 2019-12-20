using HomeControl.Finances.Domain.Entity.PurchaseAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeControl.Finances.UnitTest.Domain.Entity.PurchaseAggregate
{
    [TestClass]
    public class PurchaseItemTest
    {
        [TestMethod]
        public void PurchaseItem_Instantiate_NoValues_CalculateTotalValue()
        {
            PurchaseItem item = new PurchaseItem();
            Assert.AreEqual(0, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_Instantiate_Values_UnitValue_Quantity_CalculateTotalValue()
        {
            PurchaseItem item = new PurchaseItem(10, 10);
            Assert.AreEqual(100, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_Instantiate_Values_UnitValue_Quantity_NegativeValue_CalculateTotalValue()
        {
            PurchaseItem item = new PurchaseItem(-10, 10);
            Assert.AreEqual(-100, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_Instantiate_Values_UnitValue_Quantity_Discount_CalculateTotalValue()
        {
            PurchaseItem item = new PurchaseItem(10, 10, 10);
            Assert.AreEqual(90, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_Instantiate_Values_UnitValue_Quantity_Discount_NegativeValue_CalculateTotalValue()
        {
            PurchaseItem item = new PurchaseItem(-10, 10, 10);
            Assert.AreEqual(-110, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_SetQuantity_CalculateTotalvalue()
        {
            PurchaseItem item = new PurchaseItem(15, 5);
            Assert.AreEqual(75, item.TotalValue);

            item.SetQuantity(2);
            Assert.AreEqual(30, item.TotalValue);

            item.SetQuantity(-3);
            Assert.AreEqual(-45, item.TotalValue);

            item.SetQuantity(0);
            Assert.AreEqual(0, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_SetUnitValue_CalculateTotalvalue()
        {
            PurchaseItem item = new PurchaseItem(20, 2);
            Assert.AreEqual(40, item.TotalValue);

            item.SetUnitValue(5);
            Assert.AreEqual(10, item.TotalValue);

            item.SetUnitValue(-10);
            Assert.AreEqual(-20, item.TotalValue);

            item.SetUnitValue(0);
            Assert.AreEqual(0, item.TotalValue);
        }

        [TestMethod]
        public void PurchaseItem_SetDiscount_CalculateTotalvalue()
        {
            PurchaseItem item = new PurchaseItem(20, 2);
            Assert.AreEqual(40, item.TotalValue);

            item.SetDiscount(10);
            Assert.AreEqual(30, item.TotalValue);

            item.SetDiscount(-10);
            Assert.AreEqual(50, item.TotalValue);

            item.SetDiscount(0);
            Assert.AreEqual(40, item.TotalValue);
        }
    }
}
