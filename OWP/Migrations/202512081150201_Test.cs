namespace OWP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "NumberInStock", c => c.Byte(nullable: false));
            DropColumn("dbo.Books", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "NumberInStock");
            DropColumn("dbo.Books", "Price");
            DropColumn("dbo.Books", "ReleaseDate");
        }
    }
}
