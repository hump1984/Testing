namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractPostModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostCode = c.String(),
                        Desc = c.String(),
                        Price = c.Int(nullable: false),
                        Units = c.Int(nullable: false),
                        InvoiceModelNEAS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InvoiceModelNEAS", t => t.InvoiceModelNEAS_ID)
                .Index(t => t.InvoiceModelNEAS_ID);
            
            CreateTable(
                "dbo.InvoiceModelNEAS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        InvoiceNumber = c.Int(nullable: false),
                        Controlled = c.Boolean(nullable: false),
                        ControlDate = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        ApproveDate = c.DateTime(nullable: false),
                        ApprovedBy_Id = c.String(maxLength: 128),
                        BelongsToProject_Id = c.Int(),
                        ControlledBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.Projects", t => t.BelongsToProject_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ControlledBy_Id)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.BelongsToProject_Id)
                .Index(t => t.ControlledBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceModelNEAS", "ControlledBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContractPostModels", "InvoiceModelNEAS_ID", "dbo.InvoiceModelNEAS");
            DropForeignKey("dbo.InvoiceModelNEAS", "BelongsToProject_Id", "dbo.Projects");
            DropForeignKey("dbo.InvoiceModelNEAS", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InvoiceModelNEAS", new[] { "ControlledBy_Id" });
            DropIndex("dbo.InvoiceModelNEAS", new[] { "BelongsToProject_Id" });
            DropIndex("dbo.InvoiceModelNEAS", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.ContractPostModels", new[] { "InvoiceModelNEAS_ID" });
            DropTable("dbo.InvoiceModelNEAS");
            DropTable("dbo.ContractPostModels");
        }
    }
}
