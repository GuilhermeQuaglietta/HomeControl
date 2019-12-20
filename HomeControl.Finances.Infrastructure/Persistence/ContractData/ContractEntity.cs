using HomeControl.Core.Infrastructure.Contract;
using System;
using System.Collections.Generic;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractEntity : IEntity
    {
        public int ContractId { get; set; }
        public int OwnerId { get; set; }
        public int? CompanyId { get; set; }
        public int? AccountId { get; set; }
        public int? CardId { get; set; }

        public string Title { get; set; }
        public string ContractNumber { get; set; }
        public string Obs { get; set; }
        public int TransactionType { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PaymentFrequencyType { get; set; }
        public int PaymentFrequencyInterval { get; set; }
        public bool AutoPayment { get; set; }
        public int DueDay { get; set; }
        public decimal TotalValue { get; set; }

        public List<ContractItemEntity> Itens { get; set; }

        public object GetId()
        {
            return ContractId;
        }
    }
}
