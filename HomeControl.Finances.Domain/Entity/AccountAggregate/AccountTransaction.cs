using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;

namespace HomeControl.Finances.Domain.Entity.AccountAggregate
{
    public class AccountTransaction : ITransactionHeader, IEntity
    {
        public int AccountTransactionId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime ConciliationDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal TotalValue { get; set; }
        public int OriginId { get; set; }
        public int OriginType { get; set; }

        public int Id => AccountId;

        public int OwnerId { get; set; }
    }
}
