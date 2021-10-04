namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategorytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Todoes", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Todoes", "CategoryID");
            AddForeignKey("dbo.Todoes", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Todoes", new[] { "CategoryID" });
            DropColumn("dbo.Todoes", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
