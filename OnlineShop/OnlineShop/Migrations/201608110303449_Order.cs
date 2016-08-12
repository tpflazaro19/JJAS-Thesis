namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Item = c.String(),
                        Config = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderType = c.String(),
                        Quantity = c.Int(nullable: false),
                        Store = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
