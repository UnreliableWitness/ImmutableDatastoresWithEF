namespace AggregateEvent.Models
{
    public class PartStock
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public double InStock { get; set; }

        public double TotalSold { get; set; }

        public double TotalBought { get; set; }
    }
}
