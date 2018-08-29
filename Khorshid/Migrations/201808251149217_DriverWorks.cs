namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverWorks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromLocation = c.String(),
                        ToLocation = c.String(),
                        Price = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DriverWorks", "DriverId", "dbo.Drivers");
            DropIndex("dbo.DriverWorks", new[] { "DriverId" });
            DropTable("dbo.DriverWorks");
        }
    }
}
