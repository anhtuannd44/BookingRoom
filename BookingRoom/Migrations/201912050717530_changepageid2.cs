namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepageid2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pages");
            AlterColumn("dbo.Pages", "PageID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Pages", "PageID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Pages");
            AlterColumn("dbo.Pages", "PageID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Pages", "PageID");
        }
    }
}
