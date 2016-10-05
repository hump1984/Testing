namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInvoicePosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractPostModels", "InvoiceModelNEAS_ID", "dbo.InvoiceModelNEAS");
            DropIndex("dbo.ContractPostModels", new[] { "InvoiceModelNEAS_ID" });
            DropColumn("dbo.ContractPostModels", "Amount");
            DropColumn("dbo.ContractPostModels", "InvoiceModelNEAS_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractPostModels", "InvoiceModelNEAS_ID", c => c.Int());
            AddColumn("dbo.ContractPostModels", "Amount", c => c.Int(nullable: false));
            CreateIndex("dbo.ContractPostModels", "InvoiceModelNEAS_ID");
            AddForeignKey("dbo.ContractPostModels", "InvoiceModelNEAS_ID", "dbo.InvoiceModelNEAS", "ID");
        }
    }
}
