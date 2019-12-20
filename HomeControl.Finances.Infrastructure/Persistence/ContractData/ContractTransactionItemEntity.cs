namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractTransactionItemEntity
    {
        public int ContractTransactionItemId { get; set; }
        public int ContractTransactionId { get; set; }
        public int ProductId { get; set; }
        public int TransactionType { get; set; }
        public decimal TotalValue { get; set; }
    }
}
