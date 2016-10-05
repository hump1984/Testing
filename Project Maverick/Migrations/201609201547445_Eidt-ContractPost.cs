namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EidtContractPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractPostModels", "Unit", c => c.String());
            AddColumn("dbo.ContractPostModels", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.ContractPostModels", "Units");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractPostModels", "Units", c => c.Int(nullable: false));
            DropColumn("dbo.ContractPostModels", "Amount");
            DropColumn("dbo.ContractPostModels", "Unit");
        }
    }
}
