namespace AskYourMechanicDon.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageAndShipping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Image2", c => c.String());
            AddColumn("dbo.Products", "Image3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Image3");
            DropColumn("dbo.Products", "Image2");
            DropColumn("dbo.Products", "Shipping");
            DropColumn("dbo.OrderItems", "Shipping");
        }
    }
}
