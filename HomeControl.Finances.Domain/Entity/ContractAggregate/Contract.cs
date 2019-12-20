using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Finances.Domain.Entity.ContractAggregate
{
    public class Contract : ITransactionHeaderWithItens<ContractItem>
    {
        public int Id { get; private set; }
        public int OwnerId { get; private set; }
        public int? StoreId { get; private set; }
        public int? AccountId { get; private set; }
        public int? CardId { get; private set; }

        public string Title { get; set; }
        public string ContractNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType
        {
            get
            {
                var total = _itens.Sum(x => x.TransactionType == TransactionType.Credit ? x.TotalValue : -x.TotalValue);
                return total > 0 ? TransactionType.Credit : TransactionType.Debit;
            }
        }

        public DateTime BeginDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public PaymentFrequencyType PaymentFrequencyType { get; private set; }
        public int PaymentFrequencyInterval { get; private set; }
        public bool AutoPayment { get; private set; }
        public int DueDay { get; private set; }
        public decimal TotalValue { get; private set; }


        private List<ContractItem> _itens;
        public IReadOnlyCollection<ContractItem> Itens
        {
            get
            {
                return _itens.AsReadOnly();
            }
        }

        public Contract()
        {
            _itens = new List<ContractItem>();
            PaymentFrequencyType = PaymentFrequencyType.None;
        }
        public Contract(int id)
        {
            _itens = new List<ContractItem>();
            Id = id;
            PaymentFrequencyType = PaymentFrequencyType.None;
        }

        public Contract SetId(int id)
        {
            if (Id > 0)
                throw new InvalidOperationException("Once set ID can't be changed");

            Id = id;
            return this;
        }
        public Contract SetOwner(int id)
        {
            if (OwnerId > 0)
                throw new InvalidOperationException("Once set OwnerId can't be changed");

            OwnerId = id;
            return this;
        }
        public Contract SetStore(int storeId)
        {
            StoreId = storeId;
            return this;
        }
        public Contract SetAccount(int? id)
        {
            AccountId = id;
            CheckAutoPaymentOnPaymentChange();
            return this;
        }
        public Contract SetCard(int? id)
        {
            CardId = id;
            CheckAutoPaymentOnPaymentChange();
            return this;
        }
        public Contract ToggleAutoPayment(bool toggle)
        {
            if (toggle && AccountId == null && CardId == null)
                throw new InvalidOperationException("AutoPayment can only be activated with card or account info");

            AutoPayment = toggle;
            return this;
        }
        public Contract SetScheduling(DateTime beginDate, PaymentFrequencyType frequencyType, int frequencyInterval, int dueDay)
        {
            SetScheduling(beginDate, EndDate, frequencyType, frequencyInterval, dueDay);
            return this;
        }
        public Contract SetScheduling(DateTime beginDate, DateTime? endDate, PaymentFrequencyType frequencyType, int frequencyInterval, int dueDay)
        {
            if (endDate < beginDate)
                throw new InvalidOperationException("End date can't be before begin date");

            if (frequencyInterval <= 0)
                throw new ArgumentException("Frequency interval can't be less than 0");

            if (dueDay < 1 || dueDay > 31)
                throw new ArgumentException("Due day can't be less than 0 or bigger than 31");

            BeginDate = beginDate;
            EndDate = endDate;
            PaymentFrequencyInterval = frequencyInterval;
            PaymentFrequencyType = frequencyType;
            DueDay = dueDay;
            return this;
        }

        private void CheckAutoPaymentOnPaymentChange()
        {
            if (AccountId == null && CardId == null)
                ToggleAutoPayment(false);
        }

        public void AddItensList(IEnumerable<ContractItem> purchaseItens)
        {
            if (purchaseItens == null)
                throw new ArgumentNullException("purchaseItens", "Item can't be null");

            _itens = purchaseItens.ToList();
            _itens.ForEach(x => BindItem(ref x));
            CalculateTotalValue();
        }
        public void AddItem(ContractItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "Item can't be null");

            BindItem(ref item);
            _itens.Add(item);
            TotalValue += item.TotalValue;
        }
        public void RemoveItem(int index)
        {
            var item = _itens[index];
            TotalValue -= item.TotalValue;
            _itens.Remove(item);
        }
        public void UpdateItem(int index, ContractItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "Item can't be null");

            BindItem(ref item);
            var oldItem = _itens[index];
            _itens[index] = item;

            TotalValue -= oldItem.TotalValue;
            TotalValue += item.TotalValue;
        }

        private void CalculateTotalValue()
        {
            TotalValue = _itens.Sum(x => x.TotalValue);
        }
        private void BindItem(ref ContractItem item)
        {
            item.ContractId = Id;
        }
    }
}
