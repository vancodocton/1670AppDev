namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertodotable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Todoes", "UserID");
            AddForeignKey("dbo.Todoes", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Todoes", new[] { "UserID" });
            DropColumn("dbo.Todoes", "UserID");
        }
    }
}
