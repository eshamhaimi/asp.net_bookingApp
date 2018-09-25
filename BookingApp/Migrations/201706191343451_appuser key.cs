namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuserkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "isBanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "isBanned");
        }
    }
}
