using System;

namespace HomeControl.Finances.Domain.SeedWork.Transaction
{
    public interface ITransactionHeader : ITransaction
    {
        DateTime TransactionDate { get; }
    }
}
