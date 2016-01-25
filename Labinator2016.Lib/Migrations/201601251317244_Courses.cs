namespace Labinator2016.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Courses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseMachines",
                c => new
                    {
                        CourseMachineId = c.Int(nullable: false, identity: true),
                        VMId = c.String(),
                        CourseId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CourseMachineId);
            
            CreateTable(
                "dbo.CourseMachineTemps",
                c => new
                    {
                        CourseMachineTempId = c.Int(nullable: false, identity: true),
                        SessionId = c.String(),
                        VMId = c.String(),
                        VMName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CourseMachineTempId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Days = c.Int(nullable: false),
                        Hours = c.Int(nullable: false),
                        Template = c.String(),
                        StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Courses");
            DropTable("dbo.CourseMachineTemps");
            DropTable("dbo.CourseMachines");
        }
    }
}
