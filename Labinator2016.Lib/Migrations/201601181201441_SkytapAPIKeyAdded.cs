namespace Labinator2016.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkytapAPIKeyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "STAPIKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "STAPIKey");
        }
    }
}
