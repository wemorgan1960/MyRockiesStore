namespace AskYourMechanicDon.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyEmail2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            RenameColumn(table: "dbo.Orders", name: "Customer_Id", newName: "Customer_UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_Id", newName: "IX_Customer_UserId");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.String());
            AlterColumn("dbo.Customers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "UserId");
            AddForeignKey("dbo.Orders", "Customer_UserId", "dbo.Customers", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_UserId", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "UserId", c => c.String());
            AlterColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "Id");
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_UserId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.Orders", name: "Customer_UserId", newName: "Customer_Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
