using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Domain.SeedWork.Transaction;
using System;

namespace HomeControl.Finances.Domain.Entity.AccountAggregate
{
    public class AccountTransfer : ITransactionHeader, IEntity
    {
        public int AccountTransferId { get; set; }
        public int AccountTransactionFromId { get; set; }
        public int AccountTransactionToId { get; set; }
        public int OwnerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalValue { get; set; }
        public string Obs { get; set; }

        public int Id => AccountTransferId;
    }
}
