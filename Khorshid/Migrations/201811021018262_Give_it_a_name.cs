namespace Khorshid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Give_it_a_name : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriveName = c.String(),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        Adreess = c.String(),
                        Phone = c.String(),
                        SchoolName = c.String(),
                        SchoolAdreess = c.String(),
                        SchoolPhone = c.String(),
                        PricePay = c.Int(nullable: false),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
        }
    }
}
