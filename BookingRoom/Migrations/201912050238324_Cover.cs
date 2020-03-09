namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cover : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Cover", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Cover");
        }
    }
}
