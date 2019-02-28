namespace ZPO_Projekt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdToFoodInOrder : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.FoodInOrders");

            CreateTable(
                "dbo.FoodInOrders",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    OrderId = c.String(nullable: false, maxLength: 128),
                    FoodId = c.String(nullable: false, maxLength: 128)
                })
                .PrimaryKey(t => new { t.Id })
                .Index(t => t.OrderId);
        }
    }
}
