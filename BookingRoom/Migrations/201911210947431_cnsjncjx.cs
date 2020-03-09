namespace BookingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cnsjncjx : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Houses", name: "WardID", newName: "Id");
            RenameIndex(table: "dbo.Houses", name: "IX_WardID", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Houses", name: "IX_Id", newName: "IX_WardID");
            RenameColumn(table: "dbo.Houses", name: "Id", newName: "WardID");
        }
    }
}
