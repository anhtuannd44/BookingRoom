namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTableBookingInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingContacts", "Messenger", c => c.String());
            DropColumn("dbo.BookingContacts", "Messeger");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookingContacts", "Messeger", c => c.String());
            DropColumn("dbo.BookingContacts", "Messenger");
        }
    }
}
