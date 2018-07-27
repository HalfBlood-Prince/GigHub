namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OrginalDateTime = c.DateTime(),
                        OrginalVanue = c.String(),
                        GigId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => t.GigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "GigId", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "GigId" });
            DropTable("dbo.Notifications");
        }
    }
}
