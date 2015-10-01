namespace ProjetoIntranet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes_3009 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NewsDepartament", newName: "DepartamentNews");
            DropIndex("dbo.DepartamentNews", new[] { "NewsRefId" });
            DropIndex("dbo.DepartamentNews", new[] { "DepartamentRefId" });
            DropPrimaryKey("dbo.DepartamentNews");
            AddPrimaryKey("dbo.DepartamentNews", new[] { "DepartamentRefID", "NewsRefID" });
            CreateIndex("dbo.DepartamentNews", "DepartamentRefID");
            CreateIndex("dbo.DepartamentNews", "NewsRefID");
            DropColumn("dbo.News", "CreationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "CreationDate", c => c.DateTime(nullable: false));
            DropIndex("dbo.DepartamentNews", new[] { "NewsRefID" });
            DropIndex("dbo.DepartamentNews", new[] { "DepartamentRefID" });
            DropPrimaryKey("dbo.DepartamentNews");
            AddPrimaryKey("dbo.DepartamentNews", new[] { "NewsRefId", "DepartamentRefId" });
            CreateIndex("dbo.DepartamentNews", "DepartamentRefId");
            CreateIndex("dbo.DepartamentNews", "NewsRefId");
            RenameTable(name: "dbo.DepartamentNews", newName: "NewsDepartament");
        }
    }
}
