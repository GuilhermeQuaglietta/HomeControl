using System;

namespace HomeControl.Finances.Domain.Entity.CardAggregate
{
    public class Card
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public CardType CardType { get; set; }
        public string Title { get; set; }
        public DateTime ValidDue { get; set; }
        public int DueDate { get; set; }
        public int CloseDate { get; set; }
    }
}
