namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHomepage2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Homepages",
                c => new
                    {
                        HomepageID = c.Int(nullable: false, identity: true),
                        Hotline = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        FacebookLink = c.String(),
                        YoutubeLink = c.String(),
                        GoogleLink = c.String(),
                    })
                .PrimaryKey(t => t.HomepageID);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        SliderID = c.Int(nullable: false, identity: true),
                        Picture = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        ButtonLink1 = c.String(),
                        ButtonLink2 = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SliderID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sliders");
            DropTable("dbo.Homepages");
        }
    }
}
