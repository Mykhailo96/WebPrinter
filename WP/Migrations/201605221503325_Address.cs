namespace WP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.Purchases", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
