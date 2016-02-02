namespace Labinator2016.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Classrooms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        ClassroomId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        DataCenterId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Project = c.String(),
                        Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClassroomId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Classrooms");
        }
    }
}
