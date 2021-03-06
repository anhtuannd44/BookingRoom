namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddButton : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "ButtonContent1", c => c.String());
            AddColumn("dbo.Sliders", "ButtonContent2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "ButtonContent2");
            DropColumn("dbo.Sliders", "ButtonContent1");
        }
    }
}
