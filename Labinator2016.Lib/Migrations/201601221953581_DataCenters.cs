namespace Labinator2016.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataCenters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataCenters",
                c => new
                    {
                        DataCenterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Timezone = c.String(),
                        Type = c.Boolean(nullable: false),
                        GateWayIP = c.String(),
                    })
                .PrimaryKey(t => t.DataCenterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataCenters");
        }
    }
}
