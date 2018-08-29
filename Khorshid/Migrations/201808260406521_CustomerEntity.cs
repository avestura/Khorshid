namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CustomerEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SubscriptionId = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        MobileNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
