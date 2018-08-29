namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueIndexSubscriptionId : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Customers", "SubscriptionId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "SubscriptionId" });
        }
    }
}
