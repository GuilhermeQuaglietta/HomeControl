using HomeControl.Finances.Domain.Entity.PurchaseAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HomeControl.Finances.UnitTest.Domain.Entity.PurchaseAggregate
{
    [TestClass]
    public class PurchaseTest
    {

        [TestMethod]
        public void Purchase_Constructor_Parameterless_AssertEmptyValues()
        {
            Purchase p = new Purchase();
            Assert.AreEqual(0, p.Id);
            Assert.AreEqual(DateTime.MinValue, p.Date);
            Assert.AreEqual(0, p.TotalValue);
            Assert.AreEqual(null, p.Obs);

            Assert.AreEqual(null, p.AccountId);
            Assert.AreEqual(null, p.StoreId);
            Assert.AreEqual(null, p.CardId);
            Assert.AreEqual(null, p.Installments);

            Assert.IsNotNull(p.Itens);
            Assert.AreEqual(0, p.Itens.Count);
        }

        [TestMethod]
        public void Purchase_AddItem_Assert_TotalValue_ItemCount()
        {
            Purchase p = new Purchase();

            p.AddItem(new PurchaseItem(100, 2));
            Assert.AreEqual(200, p.TotalValue);
            Assert.AreEqual(1, p.Itens.Count);

            p.AddItem(new PurchaseItem(50, 2));
            Assert.AreEqual(300, p.TotalValue);
            Assert.AreEqual(2, p.Itens.Count);
        }

        [TestMethod]
        public void Purchase_UpdateItem_Assert_TotalValue_ItemCount()
        {
            Purchase p = new Purchase();

            p.AddItem(new PurchaseItem(10, 1));
            p.AddItem(new PurchaseItem(20, 2));
            Assert.AreEqual(50, p.TotalValue);
            Assert.AreEqual(2, p.Itens.Count);

            p.UpdateItem(0, new PurchaseItem(50, 4));
            Assert.AreEqual(240, p.TotalValue);
            Assert.AreEqual(2, p.Itens.Count);

            Assert.AreEqual(50, p.Itens.ElementAt(0).UnitValue);  //Verifica se o item continua na mesma posição
            Assert.AreEqual(4, p.Itens.ElementAt(0).Quantity);      //Verifica se o item continua na mesma posição

            p.UpdateItem(1, new PurchaseItem(30, 5));
            Assert.AreEqual(350, p.TotalValue);
            Assert.AreEqual(2, p.Itens.Count);

            Assert.AreEqual(30, p.Itens.ElementAt(1).UnitValue);    //Verifica se o item continua na mesma posição
            Assert.AreEqual(5, p.Itens.ElementAt(1).Quantity);      //Verifica se o item continua na mesma posição
        }

        [TestMethod]
        public void Purchase_RemoveItem_Assert_TotalValue_ItemCount()
        {
            Purchase p = new Purchase();

            p.AddItem(new PurchaseItem(10, 1));
            p.AddItem(new PurchaseItem(20, 1));
            p.AddItem(new PurchaseItem(30, 1));
            p.AddItem(new PurchaseItem(40, 1));
            Assert.AreEqual(100, p.TotalValue);
            Assert.AreEqual(4, p.Itens.Count);

            p.RemoveItem(0);
            Assert.AreEqual(90, p.TotalValue);
            Assert.AreEqual(3, p.Itens.Count);

            p.RemoveItem(2);
            Assert.AreEqual(50, p.TotalValue);
            Assert.AreEqual(2, p.Itens.Count);

            p.RemoveItem(1);
            Assert.AreEqual(20, p.TotalValue);
            Assert.AreEqual(1, p.Itens.Count);

            p.RemoveItem(0);
            Assert.AreEqual(0, p.TotalValue);
            Assert.AreEqual(0, p.Itens.Count);
        }
    }
}