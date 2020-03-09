namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ancncnkdkdk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Houses", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Houses", "UserID");
        }
    }
}
