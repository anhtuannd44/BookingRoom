namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigForPMToolcc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String());
        }
    }
}
