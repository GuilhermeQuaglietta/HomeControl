namespace HomeControl.Finances.Domain.Entity.ProductAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int ProductTypeId { get; set; }

        public string Title { get; set; }
        public decimal Size { get; set; }
        public string MeasureUnit { get; set; }
    }
}
