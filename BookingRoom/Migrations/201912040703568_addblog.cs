namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        BlogCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogID)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryID, cascadeDelete: true)
                .Index(t => t.BlogCategoryID);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        BlogCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BlogCategory_BlogCategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.BlogCategoryID)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategory_BlogCategoryID)
                .Index(t => t.BlogCategory_BlogCategoryID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.PageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "BlogCategoryID", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "BlogCategory_BlogCategoryID", "dbo.BlogCategories");
            DropIndex("dbo.BlogCategories", new[] { "BlogCategory_BlogCategoryID" });
            DropIndex("dbo.Blogs", new[] { "BlogCategoryID" });
            DropTable("dbo.Pages");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.Blogs");
        }
    }
}
