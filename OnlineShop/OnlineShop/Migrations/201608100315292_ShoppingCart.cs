namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingCarts");
        }
    }
}
