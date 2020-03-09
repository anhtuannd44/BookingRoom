namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "RentType");
        }
    }
}
