namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chsnc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Status", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
