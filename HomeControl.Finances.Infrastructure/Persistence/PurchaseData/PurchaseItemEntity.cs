namespace HomeControl.Finances.Domain.Entity.PurchaseAggregate
{
    public class PurchaseItemEntity
    {
        public int PurchaseItemId { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValue { get; set; }
    }
}
