namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class various_db_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceModelNEAS", "BelongsToProject_Id", "dbo.Projects");
            DropForeignKey("dbo.InvoicePosts", "ContractPost_ID", "dbo.ContractPostModels");
            DropForeignKey("dbo.InvoicePosts", "InvoiceModel_ID", "dbo.InvoiceModelNEAS");
            DropIndex("dbo.InvoiceModelNEAS", new[] { "BelongsToProject_Id" });
            DropIndex("dbo.InvoicePosts", new[] { "ContractPost_ID" });
            DropIndex("dbo.InvoicePosts", new[] { "InvoiceModel_ID" });
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "ApprovedBy_Id", newName: "ApproveByUser_Id");
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "BelongsToProject_Id", newName: "ProjectID");
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "ControlledBy_Id", newName: "ControlledByUser_Id");
            RenameColumn(table: "dbo.InvoicePosts", name: "ContractPost_ID", newName: "ContractPostID");
            RenameColumn(table: "dbo.InvoicePosts", name: "InvoiceModel_ID", newName: "InvoiceID");
            RenameIndex(table: "dbo.InvoiceModelNEAS", name: "IX_ApprovedBy_Id", newName: "IX_ApproveByUser_Id");
            RenameIndex(table: "dbo.InvoiceModelNEAS", name: "IX_ControlledBy_Id", newName: "IX_ControlledByUser_Id");
            AddColumn("dbo.ContractPostModels", "Header", c => c.String());
            AddColumn("dbo.InvoiceModelNEAS", "CreatedByUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.InvoiceModelNEAS", "ProjectID", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoicePosts", "ContractPostID", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoicePosts", "InvoiceID", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceModelNEAS", "ProjectID");
            CreateIndex("dbo.InvoiceModelNEAS", "CreatedByUser_Id");
            CreateIndex("dbo.InvoicePosts", "InvoiceID");
            CreateIndex("dbo.InvoicePosts", "ContractPostID");
            AddForeignKey("dbo.InvoiceModelNEAS", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.InvoiceModelNEAS", "ProjectID", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoicePosts", "ContractPostID", "dbo.ContractPostModels", "ID", cascadeDelete: true);
            AddForeignKey("dbo.InvoicePosts", "InvoiceID", "dbo.InvoiceModelNEAS", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePosts", "InvoiceID", "dbo.InvoiceModelNEAS");
            DropForeignKey("dbo.InvoicePosts", "ContractPostID", "dbo.ContractPostModels");
            DropForeignKey("dbo.InvoiceModelNEAS", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.InvoiceModelNEAS", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InvoicePosts", new[] { "ContractPostID" });
            DropIndex("dbo.InvoicePosts", new[] { "InvoiceID" });
            DropIndex("dbo.InvoiceModelNEAS", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.InvoiceModelNEAS", new[] { "ProjectID" });
            AlterColumn("dbo.InvoicePosts", "InvoiceID", c => c.Int());
            AlterColumn("dbo.InvoicePosts", "ContractPostID", c => c.Int());
            AlterColumn("dbo.InvoiceModelNEAS", "ProjectID", c => c.Int());
            DropColumn("dbo.InvoiceModelNEAS", "CreatedByUser_Id");
            DropColumn("dbo.ContractPostModels", "Header");
            RenameIndex(table: "dbo.InvoiceModelNEAS", name: "IX_ControlledByUser_Id", newName: "IX_ControlledBy_Id");
            RenameIndex(table: "dbo.InvoiceModelNEAS", name: "IX_ApproveByUser_Id", newName: "IX_ApprovedBy_Id");
            RenameColumn(table: "dbo.InvoicePosts", name: "InvoiceID", newName: "InvoiceModel_ID");
            RenameColumn(table: "dbo.InvoicePosts", name: "ContractPostID", newName: "ContractPost_ID");
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "ControlledByUser_Id", newName: "ControlledBy_Id");
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "ProjectID", newName: "BelongsToProject_Id");
            RenameColumn(table: "dbo.InvoiceModelNEAS", name: "ApproveByUser_Id", newName: "ApprovedBy_Id");
            CreateIndex("dbo.InvoicePosts", "InvoiceModel_ID");
            CreateIndex("dbo.InvoicePosts", "ContractPost_ID");
            CreateIndex("dbo.InvoiceModelNEAS", "BelongsToProject_Id");
            AddForeignKey("dbo.InvoicePosts", "InvoiceModel_ID", "dbo.InvoiceModelNEAS", "ID");
            AddForeignKey("dbo.InvoicePosts", "ContractPost_ID", "dbo.ContractPostModels", "ID");
            AddForeignKey("dbo.InvoiceModelNEAS", "BelongsToProject_Id", "dbo.Projects", "Id");
        }
    }
}
