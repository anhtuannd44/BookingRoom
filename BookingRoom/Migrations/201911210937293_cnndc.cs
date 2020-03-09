namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cnndc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Houses", "Wards_Id", "dbo.Wards");
            DropIndex("dbo.Houses", new[] { "Wards_Id" });
            DropColumn("dbo.Houses", "Ward");
            RenameColumn(table: "dbo.Houses", name: "Wards_Id", newName: "Ward");
            AlterColumn("dbo.Houses", "Ward", c => c.Int(nullable: false));
            CreateIndex("dbo.Houses", "Ward");
            AddForeignKey("dbo.Houses", "Ward", "dbo.Wards", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Houses", "Ward", "dbo.Wards");
            DropIndex("dbo.Houses", new[] { "Ward" });
            AlterColumn("dbo.Houses", "Ward", c => c.Int());
            RenameColumn(table: "dbo.Houses", name: "Ward", newName: "Wards_Id");
            AddColumn("dbo.Houses", "Ward", c => c.Int(nullable: false));
            CreateIndex("dbo.Houses", "Wards_Id");
            AddForeignKey("dbo.Houses", "Wards_Id", "dbo.Wards", "Id");
        }
    }
}
