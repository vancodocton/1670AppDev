namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategorytable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Todoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Todoes", new[] { "UserId" });
            DropColumn("dbo.Todoes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Todoes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Todoes", "UserId");
            AddForeignKey("dbo.Todoes", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
