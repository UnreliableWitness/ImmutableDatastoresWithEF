using System;

namespace AggregateEvent.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        public double Quantity { get; set; }

        public DateTime TimeOfSale { get; set; }

        public string TransactionId { get; set; }

    }
}
