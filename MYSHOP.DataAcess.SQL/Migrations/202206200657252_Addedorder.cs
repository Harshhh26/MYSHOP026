namespace MYSHOP.DataAcess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Email", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            DropColumn("dbo.Orders", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Name", c => c.String());
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "Email");
        }
    }
}
