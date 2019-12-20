using System;

namespace HomeControl.Finances.Domain.Entity.CardAggregate
{
    public class CardTransactionEntity
    {
        public int CardTransactionId { get; set; }
        public int CardId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DueDate { get; set; }
        public int AccountId { get; set; }
        public int OriginId { get; set; }
        public string Info { get; set; }
        public int TransactionType { get; set; }
        public decimal TransactionValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}
