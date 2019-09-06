namespace ProjectManagementSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProjTaskNotesAndPercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjTasks", "Notes", c => c.String());
            AddColumn("dbo.ProjTasks", "CompletionPercentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjTasks", "CompletionPercentage");
            DropColumn("dbo.ProjTasks", "Notes");
        }
    }
}
