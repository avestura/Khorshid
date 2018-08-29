namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverWorks", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverWorks", "Date");
        }
    }
}
