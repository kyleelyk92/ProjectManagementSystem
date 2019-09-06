namespace ProjectManagementSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeadline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Deadline");
        }
    }
}
