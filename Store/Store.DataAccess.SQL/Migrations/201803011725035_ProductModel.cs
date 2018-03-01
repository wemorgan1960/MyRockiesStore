namespace AskYourMechanicDon.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Size", c => c.String());
            AddColumn("dbo.Products", "Location", c => c.String());
            AddColumn("dbo.Products", "ShippingTerms", c => c.String());
            AddColumn("dbo.Products", "Sold", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "DateSold", c => c.DateTime());
            DropColumn("dbo.OrderItems", "Image3");
            DropColumn("dbo.Products", "Image3");
            DropColumn("dbo.Products", "VIN");
            DropColumn("dbo.Products", "Question");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Question", c => c.String());
            AddColumn("dbo.Products", "VIN", c => c.String());
            AddColumn("dbo.Products", "Image3", c => c.String());
            AddColumn("dbo.OrderItems", "Image3", c => c.String());
            DropColumn("dbo.Products", "DateSold");
            DropColumn("dbo.Products", "Sold");
            DropColumn("dbo.Products", "ShippingTerms");
            DropColumn("dbo.Products", "Location");
            DropColumn("dbo.Products", "Size");
        }
    }
}
