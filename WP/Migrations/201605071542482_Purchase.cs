namespace WP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Purchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        ObjectPrecision = c.Int(nullable: false),
                        ObjecttColor = c.Int(nullable: false),
                        ObjectMaterial = c.Int(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        FileName = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Purchases", new[] { "ApplicationUserID" });
            DropTable("dbo.Purchases");
        }
    }
}
