namespace ZPO_Projekt.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrationX : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Foods", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
