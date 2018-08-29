namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class WorkPageSecondMigration : DbMigration
    {
        public override void Up()
        {
            Sql(@"insert into dbo.WorkPages (CommissionPercentage, IsClosed, DateClosed, DriverId) select 0, 0, null, DriverId from dbo.DriverWorks");
            Sql(@"update dbo.DriverWorks set WorkPageId = WP.Id from dbo.DriverWorks DW join dbo.WorkPages WP on DW.DriverId = WP.DriverId");

            DropForeignKey("dbo.DriverWorks", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.WorkPages", "DriverId", "dbo.Drivers");
            DropIndex("dbo.DriverWorks", new[] { "DriverId" });
            DropIndex("dbo.WorkPages", new[] { "DriverId" });
            AlterColumn("dbo.WorkPages", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkPages", "DriverId");
            AddForeignKey("dbo.WorkPages", "DriverId", "dbo.Drivers", "Id", cascadeDelete: true);
            DropColumn("dbo.DriverWorks", "DriverId");

        }

        public override void Down()
        {
            AddColumn("dbo.DriverWorks", "DriverId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WorkPages", "DriverId", "dbo.Drivers");
            DropIndex("dbo.WorkPages", new[] { "DriverId" });
            AlterColumn("dbo.WorkPages", "DriverId", c => c.Int());
            CreateIndex("dbo.WorkPages", "DriverId");
            CreateIndex("dbo.DriverWorks", "DriverId");
            AddForeignKey("dbo.WorkPages", "DriverId", "dbo.Drivers", "Id");
            AddForeignKey("dbo.DriverWorks", "DriverId", "dbo.Drivers", "Id", cascadeDelete: true);
        }
    }
}
