namespace ProjectManagementSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectToTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjTasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjTasks", new[] { "Project_Id" });
            RenameColumn(table: "dbo.ProjTasks", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.ProjTasks", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjTasks", "ProjectId");
            AddForeignKey("dbo.ProjTasks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjTasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjTasks", new[] { "ProjectId" });
            AlterColumn("dbo.ProjTasks", "ProjectId", c => c.Int());
            RenameColumn(table: "dbo.ProjTasks", name: "ProjectId", newName: "Project_Id");
            CreateIndex("dbo.ProjTasks", "Project_Id");
            AddForeignKey("dbo.ProjTasks", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
