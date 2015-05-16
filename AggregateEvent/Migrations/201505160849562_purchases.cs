namespace AggregateEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchases : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Deliveries", newName: "Purchases");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Purchases", newName: "Deliveries");
        }
    }
}
