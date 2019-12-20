using System;
using System.Collections.Generic;

namespace HomeControl.Finances.Domain.Entity.PurchaseAggregate
{
    public class PurchaseEntity
    {
        public int PurchaseId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalValue { get; set; }
        public string Obs { get; set; }
        public int? StoreId { get; set; }
        public int? AccountId { get; set; }
        public int? CardId { get; set; }
        public int? Installments { get; set; }
        public List<PurchaseItemEntity> Itens { get; set; }
    }
}
