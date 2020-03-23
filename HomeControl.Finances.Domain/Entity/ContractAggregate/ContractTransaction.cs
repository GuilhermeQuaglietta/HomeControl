using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Finances.Domain.Entity.ContractAggregate
{
    public class ContractTransaction : ITransactionHeaderWithItens<ContractTransactionItem>
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public int? AccountId { get; set; }
        public int? CardId { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReferenceDate { get; set; }

        public string Obs { get; set; }
        public decimal TotalValue { get; set; }
        public TransactionType TransactionType
        {
            get
            {
                var total = _itens.Sum(x => x.TransactionType == TransactionType.Credit ? x.TotalValue : -x.TotalValue);
                return total > 0 ? TransactionType.Credit : TransactionType.Debit;
            }
        }

        private List<ContractTransactionItem> _itens;
        public IReadOnlyCollection<ContractTransactionItem> Itens
        {
            get
            {
                return _itens.AsReadOnly();
            }
        }

        public void AddItensList(IEnumerable<ContractTransactionItem> purchaseItens)
        {
            if (purchaseItens == null)
                throw new ArgumentNullException(nameof(purchaseItens));

            _itens = purchaseItens.ToList();
            _itens.ForEach(x => BindItem(ref x));
            CalculateTotalValue();
        }
        public void AddItem(ContractTransactionItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

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
        public void UpdateItem(int index, ContractTransactionItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

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
        private void BindItem(ref ContractTransactionItem item)
        {
            item.ContractTransactionId = Id;
        }
    }
}
