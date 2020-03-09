namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigForPMToolalaa : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sliders");
            AlterColumn("dbo.Sliders", "SliderID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sliders", "SliderID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sliders");
            AlterColumn("dbo.Sliders", "SliderID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sliders", "SliderID");
        }
    }
}
