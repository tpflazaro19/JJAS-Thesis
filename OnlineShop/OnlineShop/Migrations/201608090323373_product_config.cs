namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_config : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Config", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Config");
        }
    }
}
