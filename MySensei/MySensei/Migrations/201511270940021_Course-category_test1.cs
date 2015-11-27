namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coursecategory_test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppCourseStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AppCourses", "AppCourseStatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.AppCourses", "AppCourseStatusID");
            AddForeignKey("dbo.AppCourses", "AppCourseStatusID", "dbo.AppCourseStatus", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppCourses", "AppCourseStatusID", "dbo.AppCourseStatus");
            DropIndex("dbo.AppCourses", new[] { "AppCourseStatusID" });
            DropColumn("dbo.AppCourses", "AppCourseStatusID");
            DropTable("dbo.AppCourseStatus");
        }
    }
}
