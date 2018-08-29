namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkPag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommissionPercentage = c.Byte(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        DateClosed = c.DateTime(),
                        DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .Index(t => t.DriverId);
            
            AddColumn("dbo.DriverWorks", "WorkPageId", c => c.Int());
            CreateIndex("dbo.DriverWorks", "WorkPageId");
            AddForeignKey("dbo.DriverWorks", "WorkPageId", "dbo.WorkPages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DriverWorks", "WorkPageId", "dbo.WorkPages");
            DropForeignKey("dbo.WorkPages", "DriverId", "dbo.Drivers");
            DropIndex("dbo.WorkPages", new[] { "DriverId" });
            DropIndex("dbo.DriverWorks", new[] { "WorkPageId" });
            DropColumn("dbo.DriverWorks", "WorkPageId");
            DropTable("dbo.WorkPages");
        }
    }
}
