namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DistrictID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.DistrictID);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Intro = c.String(maxLength: 500),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Street = c.String(nullable: false, maxLength: 50),
                        Ward = c.Int(nullable: false),
                        Wards_Id = c.Int(),
                    })
                .PrimaryKey(t => t.HouseID)
                .ForeignKey("dbo.Wards", t => t.Wards_Id)
                .Index(t => t.Wards_Id);
            
            CreateTable(
                "dbo.PictureHouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        HouseID = c.Int(nullable: false),
                        IsMainPicture = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Houses", t => t.HouseID, cascadeDelete: true)
                .Index(t => t.HouseID);
            
            CreateTable(
                "dbo.RelaHotelServices",
                c => new
                    {
                        HouseID = c.Int(nullable: false),
                        ServiceID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.HouseID, t.ServiceID })
                .ForeignKey("dbo.Houses", t => t.HouseID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.HouseID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        TypeOfRoomID = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        SalePrice = c.Single(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        HouseID = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomID)
                .ForeignKey("dbo.Houses", t => t.HouseID, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomID, cascadeDelete: true)
                .Index(t => t.TypeOfRoomID)
                .Index(t => t.HouseID);
            
            CreateTable(
                "dbo.PictureRooms",
                c => new
                    {
                        PictureRoomID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RoomID = c.Int(nullable: false),
                        IsMainPicture = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PictureRoomID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.TypeOfRooms",
                c => new
                    {
                        TypeOfRoomID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        SingleBed = c.Int(nullable: false),
                        TwinBed = c.Int(nullable: false),
                        DOMBed = c.Int(nullable: false),
                        ChildrenMax = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TypeOfRoomID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Order_ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Order_CheckIn = c.DateTime(nullable: false),
                        Order_CheckOut = c.DateTime(nullable: false),
                        Order_PeopleName = c.String(nullable: false),
                        Order_PhoneNumber = c.String(nullable: false),
                        Order_RoomCount = c.Int(nullable: false),
                        Order_TimeBooking = c.DateTime(nullable: false),
                        Order_Status = c.String(nullable: false),
                        Order_Email = c.String(nullable: false),
                        Order_Payment = c.String(nullable: false),
                        Order_Price = c.Single(nullable: false),
                        Order_PriceSale = c.Single(nullable: false),
                        Order_PriceTotal = c.Single(nullable: false),
                        RelaHotelRoom_ID = c.Int(nullable: false),
                        Order_FreeCancel = c.Boolean(nullable: false),
                        Order_LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Order_ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Post_ID = c.Int(nullable: false, identity: true),
                        Post_Title = c.String(nullable: false),
                        Post_Content = c.String(nullable: false),
                        Post_Time = c.DateTime(nullable: false),
                        Post_Status = c.String(nullable: false),
                        Post_Cover = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Post_ID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Star = c.Int(nullable: false),
                        Comment = c.String(),
                        User_ID = c.String(),
                        RoomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Houses", "Wards_Id", "dbo.Wards");
            DropForeignKey("dbo.Rooms", "TypeOfRoomID", "dbo.TypeOfRooms");
            DropForeignKey("dbo.PictureRooms", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "HouseID", "dbo.Houses");
            DropForeignKey("dbo.RelaHotelServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.RelaHotelServices", "HouseID", "dbo.Houses");
            DropForeignKey("dbo.PictureHouses", "HouseID", "dbo.Houses");
            DropForeignKey("dbo.Wards", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Districts", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Review", new[] { "RoomID" });
            DropIndex("dbo.PictureRooms", new[] { "RoomID" });
            DropIndex("dbo.Rooms", new[] { "HouseID" });
            DropIndex("dbo.Rooms", new[] { "TypeOfRoomID" });
            DropIndex("dbo.RelaHotelServices", new[] { "ServiceID" });
            DropIndex("dbo.RelaHotelServices", new[] { "HouseID" });
            DropIndex("dbo.PictureHouses", new[] { "HouseID" });
            DropIndex("dbo.Houses", new[] { "Wards_Id" });
            DropIndex("dbo.Wards", new[] { "DistrictID" });
            DropIndex("dbo.Districts", new[] { "ProvinceId" });
            DropTable("dbo.Review");
            DropTable("dbo.Post");
            DropTable("dbo.Order");
            DropTable("dbo.TypeOfRooms");
            DropTable("dbo.PictureRooms");
            DropTable("dbo.Rooms");
            DropTable("dbo.Services");
            DropTable("dbo.RelaHotelServices");
            DropTable("dbo.PictureHouses");
            DropTable("dbo.Houses");
            DropTable("dbo.Wards");
            DropTable("dbo.Provinces");
            DropTable("dbo.Districts");
        }
    }
}
