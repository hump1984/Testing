namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditHourModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hours", "currentViewDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Hours", "currentWeek", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hours", "currentWeek");
            DropColumn("dbo.Hours", "currentViewDate");
        }
    }
}
