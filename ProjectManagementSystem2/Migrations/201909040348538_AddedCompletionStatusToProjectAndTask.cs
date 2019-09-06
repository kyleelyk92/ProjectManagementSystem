namespace ProjectManagementSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompletionStatusToProjectAndTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CompletionStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProjTasks", "CompletionStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjTasks", "CompletionStatus");
            DropColumn("dbo.Projects", "CompletionStatus");
        }
    }
}
