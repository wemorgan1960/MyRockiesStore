namespace AskYourMechanicDon.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PendingSold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PendingSold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PendingSold");
        }
    }
}
