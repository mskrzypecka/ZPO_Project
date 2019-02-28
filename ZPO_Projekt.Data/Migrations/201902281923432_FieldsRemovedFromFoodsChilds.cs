namespace ZPO_Projekt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldsRemovedFromFoodsChilds : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Foods", "ContainsMilk");
            DropColumn("dbo.Foods", "IsSpicy");
            DropColumn("dbo.Foods", "IsOriental");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "IsOriental", c => c.Boolean());
            AddColumn("dbo.Foods", "IsSpicy", c => c.Boolean());
            AddColumn("dbo.Foods", "ContainsMilk", c => c.Boolean());
        }
    }
}
