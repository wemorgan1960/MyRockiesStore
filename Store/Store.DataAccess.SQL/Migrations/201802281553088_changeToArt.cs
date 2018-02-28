namespace AskYourMechanicDon.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToArt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BasketItems", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderItems", "Description", c => c.String());
            AddColumn("dbo.OrderItems", "Image", c => c.String());
            AddColumn("dbo.OrderItems", "Image2", c => c.String());
            AddColumn("dbo.OrderItems", "Image3", c => c.String());
            DropColumn("dbo.BasketItems", "Vin");
            DropColumn("dbo.BasketItems", "Question");
            DropColumn("dbo.OrderItems", "Vin");
            DropColumn("dbo.OrderItems", "Question");
            DropColumn("dbo.OrderItems", "AnswerSubject");
            DropColumn("dbo.OrderItems", "AnswerContent");
            DropColumn("dbo.OrderItems", "AnswerTags");
            DropColumn("dbo.OrderItems", "AnswerCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "AnswerCompleted", c => c.DateTime());
            AddColumn("dbo.OrderItems", "AnswerTags", c => c.String());
            AddColumn("dbo.OrderItems", "AnswerContent", c => c.String());
            AddColumn("dbo.OrderItems", "AnswerSubject", c => c.String());
            AddColumn("dbo.OrderItems", "Question", c => c.String());
            AddColumn("dbo.OrderItems", "Vin", c => c.String());
            AddColumn("dbo.BasketItems", "Question", c => c.String());
            AddColumn("dbo.BasketItems", "Vin", c => c.String());
            DropColumn("dbo.OrderItems", "Image3");
            DropColumn("dbo.OrderItems", "Image2");
            DropColumn("dbo.OrderItems", "Image");
            DropColumn("dbo.OrderItems", "Description");
            DropColumn("dbo.BasketItems", "Shipping");
            DropColumn("dbo.BasketItems", "Price");
        }
    }
}
