namespace ProjectManagementSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDevelopersToProjTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProjTask_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ProjTask_Id");
            AddForeignKey("dbo.AspNetUsers", "ProjTask_Id", "dbo.ProjTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProjTask_Id", "dbo.ProjTasks");
            DropIndex("dbo.AspNetUsers", new[] { "ProjTask_Id" });
            DropColumn("dbo.AspNetUsers", "ProjTask_Id");
        }
    }
}
