using HomeControl.Finances.Domain.Entity.ContractAggregate;
using HomeControl.Finances.Domain.SeedWork.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Finances.UnitTest.Domain.Entity.ContractAggregate
{
    [TestClass]
    public class ContractTest
    {
        [TestMethod]
        public void Contract_Constructor_Empty_AssertDefaultValues()
        {
            Contract contract = new Contract();

            Assert.AreEqual(0, contract.Id);
            Assert.AreEqual(0, contract.OwnerId);
            Assert.AreEqual(null, contract.StoreId);
            Assert.AreEqual(null, contract.AccountId);
            Assert.AreEqual(null, contract.CardId);

            Assert.AreEqual(DateTime.MinValue, contract.BeginDate);
            Assert.AreEqual(null, contract.EndDate);
            Assert.AreEqual(PaymentFrequencyType.None, contract.PaymentFrequencyType);
            Assert.AreEqual(0, contract.PaymentFrequencyInterval);
            Assert.AreEqual(0, contract.DueDay);
            Assert.AreEqual(false, contract.AutoPayment);

            Assert.AreEqual(null, contract.ContractNumber);
            Assert.AreEqual(null, contract.Title);
            Assert.AreEqual(0, contract.TotalValue);

            Assert.IsNotNull(contract.Itens);
            Assert.IsFalse(contract.Itens.Any());
        }
        [TestMethod]
        public void Contract_Constructor_WithId_AssertDefaultValues()
        {
            Contract contract = new Contract(1);

            Assert.AreEqual(1, contract.Id);
            Assert.AreEqual(0, contract.OwnerId);
            Assert.AreEqual(null, contract.StoreId);
            Assert.AreEqual(null, contract.AccountId);
            Assert.AreEqual(null, contract.CardId);

            Assert.AreEqual(DateTime.MinValue, contract.BeginDate);
            Assert.AreEqual(null, contract.EndDate);
            Assert.AreEqual(PaymentFrequencyType.None, contract.PaymentFrequencyType);
            Assert.AreEqual(0, contract.PaymentFrequencyInterval);
            Assert.AreEqual(0, contract.DueDay);
            Assert.AreEqual(false, contract.AutoPayment);

            Assert.AreEqual(null, contract.ContractNumber);
            Assert.AreEqual(null, contract.Title);
            Assert.AreEqual(0, contract.TotalValue);

            Assert.IsNotNull(contract.Itens);
            Assert.IsFalse(contract.Itens.Any());
        }

        [TestMethod]
        public void Contract_SetContractNumber_AssertValueIsSet()
        {
            Contract c = new Contract
            {
                ContractNumber = "ABC123"
            };
            Assert.AreEqual("ABC123", c.ContractNumber);
        }
        [TestMethod]
        public void Contract_SetTitle_AssertValueIsSet()
        {
            Contract c = new Contract
            {
                Title = "ABC123"
            };
            Assert.AreEqual("ABC123", c.Title);
        }

        [TestMethod]
        public void Contract_ToggleAutoPayment_AccountProvided_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetAccount(1);
            c.ToggleAutoPayment(true);
            Assert.IsTrue(c.AutoPayment);
        }
        [TestMethod]
        public void Contract_ToggleAutoPayment_CardProvided_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetCard(1);
            c.ToggleAutoPayment(true);
            Assert.IsTrue(c.AutoPayment);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Contract_ToggleAutoPayment_PaymentInfoNoteProvided_Throw_InvalidOperationException()
        {
            Contract c = new Contract();
            c.ToggleAutoPayment(true);
        }

        [TestMethod]
        public void Contract_RemovePaymentInfo_AutoRemoveAutoPaymentInfo()
        {
            Contract c = new Contract();
            c.SetCard(1);
            c.ToggleAutoPayment(true);
            c.SetCard(null);
            Assert.IsFalse(c.AutoPayment);
        }

        [TestMethod]
        public void Contract_SetId_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetId(1);
            Assert.AreEqual(1, c.Id);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void Contract_SetId_AssertValueCantBeSetAgain()
        {
            Contract c = new Contract();
            c.SetId(1);
            c.SetId(2);
        }

        [TestMethod]
        public void Contract_SetOwner_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetOwner(1);
            Assert.AreEqual(1, c.OwnerId);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Contract_SetOwner_AssertValueCantBeSetAgain()
        {
            Contract c = new Contract();
            c.SetOwner(1);
            c.SetOwner(2);
        }

        [TestMethod]
        public void Contract_SetAccount_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetAccount(1);
            Assert.AreEqual(1, c.AccountId);
        }

        [TestMethod]
        public void Contract_SetCard_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetCard(1);
            Assert.AreEqual(1, c.CardId);
        }

        [TestMethod]
        public void Contract_SetStore_AssertValueIsSet()
        {
            Contract c = new Contract();
            c.SetStore(1);
            Assert.AreEqual(1, c.StoreId);
        }

        [TestMethod]
        public void Contract_SetScheduling_AssertValueIsSet()
        {
            var dataAgendamento = DateTime.Now;
            Contract c = new Contract();
            c.SetScheduling(dataAgendamento, PaymentFrequencyType.Anually, 1, 1);

            Assert.AreEqual(dataAgendamento, c.BeginDate);
            Assert.IsNull(c.EndDate);
            Assert.AreEqual(PaymentFrequencyType.Anually, c.PaymentFrequencyType);
            Assert.AreEqual(1, c.PaymentFrequencyInterval);
            Assert.AreEqual(1, c.DueDay);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Contract_SetScheduling_FrequencyIntervalCantBeLowerThanZero()
        {
            var dataAgendamento = DateTime.Now;

            Contract c = new Contract();
            c.SetScheduling(dataAgendamento, PaymentFrequencyType.Anually, -1, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Contract_SetScheduling_DueDayHasToBeValidDay()
        {
            var dataAgendamento = DateTime.Now;
            Contract c = new Contract();
            c.SetScheduling(dataAgendamento, PaymentFrequencyType.Anually, 1, -1);
        }

        [TestMethod]
        public void Contract_SetScheduling_WithEndDate_AssertValueIsSet()
        {
            var beginDate = DateTime.Now;
            var endDate = beginDate.AddMonths(1);

            Contract c = new Contract();
            c.SetScheduling(beginDate, endDate, PaymentFrequencyType.Anually, 1, 1);

            Assert.AreEqual(beginDate, c.BeginDate);
            Assert.AreEqual(endDate, c.EndDate);
            Assert.AreEqual(PaymentFrequencyType.Anually, c.PaymentFrequencyType);
            Assert.AreEqual(1, c.PaymentFrequencyInterval);
            Assert.AreEqual(1, c.DueDay);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Contract_SetScheduling_WithEndDate_EndDateCantBeHigherThanBeginDate()
        {
            var endDate = DateTime.Now;
            var beginDate = endDate.AddMonths(1);

            Contract c = new Contract();
            c.SetScheduling(beginDate, endDate, PaymentFrequencyType.Anually, 1, 1);
        }


        [TestMethod]
        public void Contract_AddItemList_Assert_TotalValue_ItemCount()
        {
            Contract c = new Contract();
            List<ContractItem> itens = new List<ContractItem>()
            {
                new ContractItem(2000, TransactionType.Credit),
                new ContractItem(1000, TransactionType.Credit)
            };
            c.AddItensList(itens);

            Assert.AreEqual(3000, c.TotalValue);
            Assert.AreEqual(2, c.Itens.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contract_AddItemList_DontAcceptNullList_Throw_ArgumentNullException()
        {
            Contract c = new Contract();
            c.AddItensList(null);
        }

        [TestMethod]
        public void Contract_AddItem_Assert_TotalValue()
        {
            Contract p = new Contract();
            p.AddItem(new ContractItem(100, TransactionType.Credit));
            Assert.AreEqual(100, p.TotalValue);

            p.AddItem(new ContractItem(50, TransactionType.Credit));
            Assert.AreEqual(150, p.TotalValue);
        }
        [TestMethod]
        public void Contract_AddItem_Assert_ItemCount()
        {
            Contract p = new Contract();

            p.AddItem(new ContractItem(100, TransactionType.Credit));
            Assert.AreEqual(1, p.Itens.Count);

            p.AddItem(new ContractItem(50, TransactionType.Credit));
            Assert.AreEqual(2, p.Itens.Count);
        }
        [TestMethod]
        public void Contract_AddItem_Assert_ContractIdIsBinded()
        {
            Contract p = new Contract(1);
            p.AddItem(new ContractItem(100, TransactionType.Credit));
            p.AddItem(new ContractItem(50, TransactionType.Credit));

            Assert.IsFalse(p.Itens.Any(x => x.ContractId != p.Id));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contract_AddItem_DontAcceptNullItem_Throw_ArgumentException()
        {
            Contract p = new Contract();
            p.AddItem(null);
        }

        [TestMethod]
        public void Contract_UpdateItem_Assert_TotalValue()
        {
            Contract p = new Contract();

            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.AddItem(new ContractItem(20, TransactionType.Credit));
            Assert.AreEqual(30, p.TotalValue);

            p.UpdateItem(0, new ContractItem(50, TransactionType.Credit));
            Assert.AreEqual(70, p.TotalValue);

            p.UpdateItem(1, new ContractItem(30, TransactionType.Credit));
            Assert.AreEqual(80, p.TotalValue);
        }

        [TestMethod]
        public void Contract_UpdateItem_Assert_ItemCount()
        {
            Contract p = new Contract();

            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.AddItem(new ContractItem(20, TransactionType.Credit));
            Assert.AreEqual(2, p.Itens.Count);

            p.UpdateItem(0, new ContractItem(50, TransactionType.Credit));
            Assert.AreEqual(2, p.Itens.Count);

            p.UpdateItem(1, new ContractItem(30, TransactionType.Credit));
            Assert.AreEqual(2, p.Itens.Count);
        }

        [TestMethod]
        public void Contract_UpdateItem_Assert_ContractIdIsBinded()
        {
            Contract p = new Contract();

            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.AddItem(new ContractItem(20, TransactionType.Credit));

            p.UpdateItem(0, new ContractItem(50, TransactionType.Credit));
            p.UpdateItem(1, new ContractItem(30, TransactionType.Credit));

            Assert.IsFalse(p.Itens.Any(x => x.ContractId != p.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contract_UpdateItem_DontAcceptNullItem_Throw_ArgumentException()
        {
            Contract p = new Contract();
            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.AddItem(new ContractItem(20, TransactionType.Credit));
            p.UpdateItem(0, null);
        }

        [TestMethod]
        public void Contract_RemoveItem_Assert_TotalValue_ItemCount()
        {
            Contract p = new Contract();

            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.AddItem(new ContractItem(20, TransactionType.Credit));
            p.AddItem(new ContractItem(30, TransactionType.Credit));
            p.AddItem(new ContractItem(40, TransactionType.Credit));
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Contract_RemoveItem_IndexOutOfRange_Throw_IndexOutOfRangeException()
        {
            Contract p = new Contract();
            p.AddItem(new ContractItem(10, TransactionType.Credit));
            p.RemoveItem(1);
        }
    }
}
