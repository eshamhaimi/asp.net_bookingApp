namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrPlaces : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Places", "Region_Id", "dbo.Regions");
            DropIndex("dbo.Places", new[] { "Region_Id" });
            AlterColumn("dbo.Places", "Region_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Places", "Region_Id");
            AddForeignKey("dbo.Places", "Region_Id", "dbo.Regions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Places", "Region_Id", "dbo.Regions");
            DropIndex("dbo.Places", new[] { "Region_Id" });
            AlterColumn("dbo.Places", "Region_Id", c => c.Int());
            CreateIndex("dbo.Places", "Region_Id");
            AddForeignKey("dbo.Places", "Region_Id", "dbo.Regions", "Id");
        }
    }
}
