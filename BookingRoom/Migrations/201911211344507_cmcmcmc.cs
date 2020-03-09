namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cmcmcmc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Houses", "Id", "dbo.Wards");
            DropIndex("dbo.Houses", new[] { "Id" });
            AddColumn("dbo.Houses", "Ward", c => c.String(nullable: false));
            AddColumn("dbo.Houses", "District", c => c.String(nullable: false));
            AddColumn("dbo.Houses", "Province", c => c.String(nullable: false));
            DropColumn("dbo.Houses", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Houses", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Houses", "Province");
            DropColumn("dbo.Houses", "District");
            DropColumn("dbo.Houses", "Ward");
            CreateIndex("dbo.Houses", "Id");
            AddForeignKey("dbo.Houses", "Id", "dbo.Wards", "Id", cascadeDelete: true);
        }
    }
}
