namespace WP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Purchase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Address");
            DropColumn("dbo.Purchases", "OrderNumber");
        }
    }
}
