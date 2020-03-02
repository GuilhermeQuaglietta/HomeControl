using System;

namespace HomeControl.Finances.Domain.Entity.CardAggregate
{
    public class CardEntity
    {
        public int CardId { get; set; }
        public int ownerId { get; set; }
        public int CardType { get; set; }
        public string Title { get; set; }
        public DateTime ValidDue { get; set; }
        public int DueDate { get; set; }
        public int CloseDate { get; set; }
    }
}
