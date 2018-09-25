namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Username", c => c.String(maxLength: 100));
            DropColumn("dbo.AppUsers", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "FullName", c => c.String(maxLength: 100));
            DropColumn("dbo.AppUsers", "Username");
        }
    }
}
