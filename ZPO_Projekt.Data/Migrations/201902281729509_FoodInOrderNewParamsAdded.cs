namespace ZPO_Projekt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodInOrderNewParamsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodInOrders",
                c => new
                    {
                        IdOfOrder = c.String(nullable: false, maxLength: 128),
                        IdOfFood = c.String(nullable: false, maxLength: 128),
                        Order_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdOfOrder, t.IdOfFood })
                .Index(t => t.Order_Id);
        }
        
        public override void Down()
        {
        }
    }
}
