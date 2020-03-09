namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlog2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "TimeModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Blogs", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Blogs", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Status");
            DropColumn("dbo.Blogs", "LastUpdate");
            DropColumn("dbo.Blogs", "TimeModified");
        }
    }
}
