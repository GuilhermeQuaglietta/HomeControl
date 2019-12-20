using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Finances.Domain.Entity.PurchaseAggregate
{
    public class Purchase : ITransactionHeaderWithItens<PurchaseItem>
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Obs { get; set; }
        public decimal TotalValue { get; private set; }
        public int? StoreId { get; private set; }
        public int? AccountId { get; private set; }
        public int? CardId { get; private set; }
        public int? Installments { get; private set; }
        public DateTime TransactionDate { get; set; }

        public Purchase()
        {
            _itens = new List<PurchaseItem>();
        }

        public Purchase(int id, DateTime date, int storeId)
        {
            SetId(id);
            SetDate(date);
            SetStore(storeId);
            _itens = new List<PurchaseItem>();
        }
        public Purchase(DateTime date, int storeId, int accountId)
        {
            SetDate(date);
            SetStore(storeId);
            SetAccount(accountId);
            _itens = new List<PurchaseItem>();
        }
        public Purchase(int id, DateTime date, int storeId, int accountId)
        {
            SetDate(date);
            SetStore(storeId);
            SetAccount(accountId);
            _itens = new List<PurchaseItem>();
        }
        public Purchase(DateTime date, int storeId, int cardId, int installments)
        {
            SetDate(date);
            SetStore(storeId);
            SetCard(cardId, installments);
            _itens = new List<PurchaseItem>();
        }
        public Purchase(int id, DateTime date, int storeId, int cardId, int installments)
        {
            SetId(id);
            SetDate(date);
            SetStore(storeId);
            SetCard(cardId, installments);
            _itens = new List<PurchaseItem>();
        }

        public Purchase SetId(int id)
        {
            if (Id != 0)
                throw new InvalidOperationException("Purchase id can't be changed once it's set.");
            Id = id;

            return this;
        }
        public Purchase SetStore(int id)
        {
            StoreId = id;
            return this;
        }
        public Purchase SetDate(DateTime date)
        {
            Date = date;
            return this;
        }
        public Purchase SetAccount(int id)
        {
            AccountId = id;
            return this;
        }
        public Purchase SetCard(int id, int installments)
        {
            CardId = id;
            Installments = installments;
            return this;
        }

        private List<PurchaseItem> _itens;
        public IReadOnlyCollection<PurchaseItem> Itens
        {
            get
            {
                return _itens.AsReadOnly();
            }
        }

        public TransactionType TransactionType
        {
            get
            {
                var total = _itens.Sum(x => x.TransactionType == TransactionType.Credit ? x.TotalValue : -x.TotalValue);
                return total > 0 ? TransactionType.Credit : TransactionType.Debit;
            }
        }

        public void AddItensList(IEnumerable<PurchaseItem> purchaseItens)
        {
            _itens = purchaseItens.ToList();
            CalculateTotalValue();
        }
        private void CalculateTotalValue()
        {
            TotalValue = _itens.Sum(x => x.TotalValue);
        }

        public void AddItem(PurchaseItem item)
        {
            _itens.Add(item);
            TotalValue += item.TotalValue;
        }
        public void RemoveItem(int index)
        {
            var item = _itens[index];
            TotalValue -= item.TotalValue;
            _itens.Remove(item);
        }
        public void UpdateItem(int index, PurchaseItem item)
        {
            var oldItem = _itens[index];
            _itens[index] = item;

            TotalValue -= oldItem.TotalValue;
            TotalValue += item.TotalValue;
        }
    }
}
