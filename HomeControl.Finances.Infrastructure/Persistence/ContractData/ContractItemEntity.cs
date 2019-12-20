namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractItemEntity
    {
        public int ContractItemId { get; set; }
        public int ContractId { get; set; }
        public int ProductId { get; set; }
        public int TransactionType { get; set; }
        public decimal TotalValue { get; set; }
    }
}
