namespace Project_Maverick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_price_to_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoicePosts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoicePosts", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoicePosts", "Total", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoicePosts", "Amount", c => c.Int(nullable: false));
        }
    }
}
