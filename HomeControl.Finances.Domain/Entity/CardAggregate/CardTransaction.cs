using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;

namespace HomeControl.Finances.Domain.Entity.CardAggregate
{
    public class CardTransaction : ITransactionHeader
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int AccountId { get; set; }
        public int OriginId { get; set; }

        public string Info { get; set; }

        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}
