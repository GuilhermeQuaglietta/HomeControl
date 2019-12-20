namespace HomeControl.Finances.Domain.Entity.ProductAggregate
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public int TransactionCategoryId { get; set; }
        public int ProductTypeId { get; set; }

        public string Title { get; set; }
        public decimal Size { get; set; }
        public string MeasureUnit { get; set; }
    }
}
