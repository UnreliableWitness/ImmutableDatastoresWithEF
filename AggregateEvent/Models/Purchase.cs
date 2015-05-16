using System;

namespace AggregateEvent.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        public double Amount { get; set; }

        public DateTime TimeOfPurchase { get; set; }

        public string TransactionId { get; set; }
    }
}
