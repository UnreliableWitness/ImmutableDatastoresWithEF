using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace AggregateEvent.Models
{
    public class StockContext : DbContext
    {
        public StockContext()
        {
            Database.SetInitializer(new StockContextInitializer());
        }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Purchase> Deliveries { get; set; }

        public DbSet<PartStock> PartStocks { get; set; }
    }

    public class StockContextInitializer : DropCreateDatabaseAlways<StockContext>
    {
        protected override void Seed(StockContext context)
        {

            context.Parts.AddOrUpdate(new[]{new Part
            {
                Name = "windows 10"
            },
            new Part
            {
                Name = "surface 4"
            },
            new Part
            {
                Name = "hololens"
            }, });
            base.Seed(context);
        }
    }
}
