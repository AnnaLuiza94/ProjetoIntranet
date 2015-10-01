namespace ProjetoIntranet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departament",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NewsDepartament",
                c => new
                    {
                        NewsRefId = c.Int(nullable: false),
                        DepartamentRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsRefId, t.DepartamentRefId })
                .ForeignKey("dbo.News", t => t.NewsRefId, cascadeDelete: true)
                .ForeignKey("dbo.Departament", t => t.DepartamentRefId, cascadeDelete: true)
                .Index(t => t.NewsRefId)
                .Index(t => t.DepartamentRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "UserID", "dbo.Users");
            DropForeignKey("dbo.NewsDepartament", "DepartamentRefId", "dbo.Departament");
            DropForeignKey("dbo.NewsDepartament", "NewsRefId", "dbo.News");
            DropIndex("dbo.NewsDepartament", new[] { "DepartamentRefId" });
            DropIndex("dbo.NewsDepartament", new[] { "NewsRefId" });
            DropIndex("dbo.News", new[] { "UserID" });
            DropTable("dbo.NewsDepartament");
            DropTable("dbo.Users");
            DropTable("dbo.News");
            DropTable("dbo.Departament");
        }
    }
}
