namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cnscmd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Houses", name: "Ward", newName: "WardID");
            RenameIndex(table: "dbo.Houses", name: "IX_Ward", newName: "IX_WardID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Houses", name: "IX_WardID", newName: "IX_Ward");
            RenameColumn(table: "dbo.Houses", name: "WardID", newName: "Ward");
        }
    }
}
