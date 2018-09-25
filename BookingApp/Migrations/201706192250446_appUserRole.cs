namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "Role");
        }
    }
}
