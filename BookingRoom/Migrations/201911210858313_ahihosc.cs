namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ahihosc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "MaxChildren", c => c.Int(nullable: false));
            DropColumn("dbo.TypeOfRooms", "ChildrenMax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypeOfRooms", "ChildrenMax", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "MaxChildren");
        }
    }
}
