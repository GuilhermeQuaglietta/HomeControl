using System.Collections.Generic;

namespace HomeControl.Finances.Domain.Entity.AccountAggregate
{
    public abstract class Account
    {
        public virtual int AccountId { get; set; }
        public virtual string Title { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual int HighlightColor { get; set; }
    }
}
