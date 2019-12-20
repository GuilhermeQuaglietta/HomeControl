using System;
using System.Collections.Generic;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractTransactionEntity
    {
        public int ContractTransactionId { get; set; }
        public int ContractId { get; set; }
        public int? AccountId { get; set; }
        public int? CardId { get; set; }
        public string Obs { get; set; }
        public int TransactionType { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReferenceDate { get; set; }

        public List<ContractTransactionItemEntity> Itens { get; set; }
    }
}
