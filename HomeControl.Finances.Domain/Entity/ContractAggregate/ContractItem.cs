using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;

namespace HomeControl.Finances.Domain.Entity.ContractAggregate
{
    public class ContractItem : ITransaction
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public int ProductId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal TotalValue { get; private set; }
        public ContractItem()
        {
            TransactionType = TransactionType.Credit;
        }
        public ContractItem(decimal totalValue, TransactionType transactionType)
        {
            TotalValue = totalValue;
            TransactionType = transactionType;
        }

        public ContractItem SetTotalValue(decimal value)
        {
            if (value < 0) throw new ArgumentException("Value must be 0 or a positive number", nameof(value));
            TotalValue = value;
            return this;
        }
        public ContractItem SetTotalValue(decimal value, TransactionType transactionType)
        {
            if (value < 0) throw new ArgumentException("Value must be 0 or a positive number", nameof(value));
            TotalValue = value;
            TransactionType = transactionType;
            return this;
        }
    }
}
