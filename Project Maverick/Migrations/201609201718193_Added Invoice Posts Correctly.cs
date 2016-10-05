namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInvoicePostsCorrectly : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoicePosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        InvoiceModel_ID = c.Int(),
                        InvoicePost_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InvoiceModelNEAS", t => t.InvoiceModel_ID)
                .ForeignKey("dbo.InvoicePosts", t => t.InvoicePost_ID)
                .Index(t => t.InvoiceModel_ID)
                .Index(t => t.InvoicePost_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePosts", "InvoicePost_ID", "dbo.InvoicePosts");
            DropForeignKey("dbo.InvoicePosts", "InvoiceModel_ID", "dbo.InvoiceModelNEAS");
            DropIndex("dbo.InvoicePosts", new[] { "InvoicePost_ID" });
            DropIndex("dbo.InvoicePosts", new[] { "InvoiceModel_ID" });
            DropTable("dbo.InvoicePosts");
        }
    }
}
