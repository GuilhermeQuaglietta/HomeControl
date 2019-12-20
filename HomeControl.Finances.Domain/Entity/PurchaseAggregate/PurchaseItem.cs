using HomeControl.Finances.Domain.SeedWork.Transaction;

namespace HomeControl.Finances.Domain.Entity.PurchaseAggregate
{
    public class PurchaseItem : ITransactionItem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }

        public decimal UnitValue { get; private set; }

        public decimal Quantity { get; private set; }

        public decimal Discount { get; private set; }

        private decimal? _totalValue;
        public decimal TotalValue
        {
            get
            {
                if (_totalValue == null)
                    CalculateTotalValue();

                return _totalValue.Value;
            }
        }

        public TransactionType TransactionType => TransactionType.Debit;

        public PurchaseItem()
        {

        }

        public PurchaseItem(decimal unitValue, decimal quantity)
        {
            UnitValue = unitValue;
            Quantity = quantity;
            CalculateTotalValue();
        }
        public PurchaseItem(decimal unitValue, decimal quantity, decimal discount)
        {
            UnitValue = unitValue;
            Quantity = quantity;
            Discount = discount;
            CalculateTotalValue();
        }

        public void SetUnitValue(decimal unitValue)
        {
            UnitValue = unitValue;
            CalculateTotalValue();
        }

        public void SetDiscount(decimal discountValue)
        {
            Discount = discountValue;
            CalculateTotalValue();
        }

        public void SetQuantity(decimal quantity)
        {
            Quantity = quantity;
            CalculateTotalValue();
        }

        private void CalculateTotalValue()
        {
            _totalValue = (UnitValue * Quantity) - Discount;
        }
    }
}
