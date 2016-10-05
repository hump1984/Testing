namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test31 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Hours", new[] { "project_Id" });
            DropIndex("dbo.Hours", new[] { "user_Id" });
            AlterColumn("dbo.Hours", "user_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Hours", "project_Id", c => c.Int());
            AlterColumn("dbo.Projects", "Active", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Hours", "project_Id");
            CreateIndex("dbo.Hours", "user_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Hours", new[] { "user_Id" });
            DropIndex("dbo.Hours", new[] { "project_Id" });
            AlterColumn("dbo.Projects", "Active", c => c.Int(nullable: false));
            AlterColumn("dbo.Hours", "project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Hours", "user_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Hours", "user_Id");
            CreateIndex("dbo.Hours", "project_Id");
        }
    }
}
