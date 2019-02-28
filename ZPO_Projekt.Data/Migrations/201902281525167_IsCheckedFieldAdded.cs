namespace ZPO_Projekt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCheckedFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "IsChecked");
        }
    }
}
