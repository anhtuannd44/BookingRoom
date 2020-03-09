namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "ShortDescription");
        }
    }
}
