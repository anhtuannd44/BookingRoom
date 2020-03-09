namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Houses", "Payment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Houses", "Payment");
        }
    }
}
