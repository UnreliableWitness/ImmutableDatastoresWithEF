namespace AggregateEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "TimeOfPurchase", c => c.DateTime(nullable: false));
            AddColumn("dbo.Purchases", "TransactionId", c => c.String());
            AddColumn("dbo.Sales", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "TransactionId");
            DropColumn("dbo.Purchases", "TransactionId");
            DropColumn("dbo.Purchases", "TimeOfPurchase");
        }
    }
}
