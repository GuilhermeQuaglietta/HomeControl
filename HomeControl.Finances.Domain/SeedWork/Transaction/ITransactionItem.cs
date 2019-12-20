namespace HomeControl.Finances.Domain.SeedWork.Transaction
{
    public interface ITransactionItem : ITransaction
    {
        decimal UnitValue { get; }
        decimal Quantity { get; }
    }
}
