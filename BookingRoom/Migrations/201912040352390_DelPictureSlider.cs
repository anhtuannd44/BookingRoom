namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelPictureSlider : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sliders", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sliders", "Picture", c => c.String());
        }
    }
}
