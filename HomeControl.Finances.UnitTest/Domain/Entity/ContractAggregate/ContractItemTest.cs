using HomeControl.Finances.Domain.Entity.ContractAggregate;
using HomeControl.Finances.Domain.SeedWork.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeControl.Finances.UnitTest.Domain.Entity.ContractAggregate
{
    [TestClass]
    public class ContractItemTest
    {
        [TestMethod]
        public void ContractItem_Construtor_NoValues_AssertDefaultValues()
        {
            ContractItem item = new ContractItem();

            Assert.AreEqual(0, item.Id);
            Assert.AreEqual(0, item.ContractId);
            Assert.AreEqual(0, item.ProductId);
            Assert.AreEqual(TransactionType.Credit, item.TransactionType);
            Assert.AreEqual(0, item.TotalValue);
        }

        [TestMethod]
        public void ContractItem_Construtor_Value_TransactionType_AssertDefaultValues()
        {
            ContractItem item = new ContractItem(100, TransactionType.Credit);

            Assert.AreEqual(0, item.Id);
            Assert.AreEqual(0, item.ContractId);
            Assert.AreEqual(0, item.ProductId);
            Assert.AreEqual(TransactionType.Credit, item.TransactionType);
            Assert.AreEqual(100, item.TotalValue);
        }

        [TestMethod]
        public void ContractItem_SetId_Assert_ValueIsSet()
        {
            ContractItem item = new ContractItem
            {
                Id = 1
            };
            Assert.AreEqual(1, item.Id);
        }
        [TestMethod]
        public void ContractItem_SetContractId_Assert_ValueIsSet()
        {
            ContractItem item = new ContractItem
            {
                ContractId = 1
            };
            Assert.AreEqual(1, item.ContractId);
        }
        [TestMethod]
        public void ContractItem_SetProductId_Assert_ValueIsSet()
        {
            ContractItem item = new ContractItem
            {
                ProductId = 1
            };
            Assert.AreEqual(1, item.ProductId);
        }
        [TestMethod]
        public void ContractItem_SetTotalValue_Assert_ValueIsSet()
        {
            ContractItem item = new ContractItem(100, TransactionType.Credit);
            Assert.AreEqual(100, item.TotalValue);
        }
        [TestMethod]
        public void ContractItem_SetTransactionType_Assert_ValueIsSet()
        {
            ContractItem item = new ContractItem
            {
                TransactionType = TransactionType.Credit
            };
            Assert.AreEqual(TransactionType.Credit, item.TransactionType);
        }
    }
}
