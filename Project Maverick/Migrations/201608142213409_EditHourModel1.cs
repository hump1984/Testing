namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditHourModel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hours", "currentViewDate");
            DropColumn("dbo.Hours", "currentWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hours", "currentWeek", c => c.Int(nullable: false));
            AddColumn("dbo.Hours", "currentViewDate", c => c.DateTime(nullable: false));
        }
    }
}
