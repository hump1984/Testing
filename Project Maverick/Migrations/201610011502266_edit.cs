namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoicePosts", "InvoicePost_ID", "dbo.InvoicePosts");
            DropIndex("dbo.InvoicePosts", new[] { "InvoicePost_ID" });
            AddColumn("dbo.InvoicePosts", "ContractPost_ID", c => c.Int());
            CreateIndex("dbo.InvoicePosts", "ContractPost_ID");
            AddForeignKey("dbo.InvoicePosts", "ContractPost_ID", "dbo.ContractPostModels", "ID");
            DropColumn("dbo.InvoicePosts", "InvoicePost_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoicePosts", "InvoicePost_ID", c => c.Int());
            DropForeignKey("dbo.InvoicePosts", "ContractPost_ID", "dbo.ContractPostModels");
            DropIndex("dbo.InvoicePosts", new[] { "ContractPost_ID" });
            DropColumn("dbo.InvoicePosts", "ContractPost_ID");
            CreateIndex("dbo.InvoicePosts", "InvoicePost_ID");
            AddForeignKey("dbo.InvoicePosts", "InvoicePost_ID", "dbo.InvoicePosts", "ID");
        }
    }
}
