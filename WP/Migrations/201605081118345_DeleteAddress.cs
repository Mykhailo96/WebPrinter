namespace WP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
    }
}
