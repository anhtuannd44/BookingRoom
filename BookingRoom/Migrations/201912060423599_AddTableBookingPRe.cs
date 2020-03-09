namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableBookingPRe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingContacts",
                c => new
                    {
                        BookingContactID = c.Int(nullable: false, identity: true),
                        PersonName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        RoomID = c.Int(nullable: false),
                        RoomCount = c.Int(nullable: false),
                        Messeger = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookingContactID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingContacts", "RoomID", "dbo.Rooms");
            DropIndex("dbo.BookingContacts", new[] { "RoomID" });
            DropTable("dbo.BookingContacts");
        }
    }
}
