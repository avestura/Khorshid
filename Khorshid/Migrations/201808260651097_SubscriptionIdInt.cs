namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscriptionIdInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "SubscriptionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "SubscriptionId", c => c.String());
        }
    }
}
