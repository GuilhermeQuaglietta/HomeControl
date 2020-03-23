using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;

namespace HomeControl.Finances.Domain.Entity.ContractAggregate
{
    public class ContractTransactionItem : ITransaction
    {
        public int Id { get; set; }
        public int ContractTransactionId { get; set; }

        public string Title { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal TotalValue { get; private set; }

        public ContractTransactionItem()
        {
            TransactionType = TransactionType.Credit;
        }
        public ContractTransactionItem(decimal totalValue, TransactionType transactionType)
        {
            TotalValue = totalValue;
            TransactionType = transactionType;
        }

        public ContractTransactionItem SetTotalValue(decimal value)
        {
            return SetTotalValue(value, TransactionType);
        }
        public ContractTransactionItem SetTotalValue(decimal value, TransactionType transactionType)
        {
            if (value < 0) throw new ArgumentException("Value must be 0 or a positive number", nameof(value));
            TotalValue = value;
            TransactionType = transactionType;
            return this;
        }
    }
}
