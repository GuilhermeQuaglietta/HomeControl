using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.Finances.Domain.Entity.AccountAggregate
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeDescription { get; set; }
        public DateTime? IncludedDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public int OwnerId { get; set; }

    }
}
