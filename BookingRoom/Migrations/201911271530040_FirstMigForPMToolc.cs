namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigForPMToolc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Name");
        }
    }
}
