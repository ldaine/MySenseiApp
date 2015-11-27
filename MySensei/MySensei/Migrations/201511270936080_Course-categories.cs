namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coursecategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppCourses", "AppCategory_ID", "dbo.AppCategories");
            DropIndex("dbo.AppCourses", new[] { "AppCategory_ID" });
            RenameColumn(table: "dbo.AppCourses", name: "AppCategory_ID", newName: "AppCategoryID");
            CreateTable(
                "dbo.AppTagAppCourses",
                c => new
                    {
                        AppTag_ID = c.Int(nullable: false),
                        AppCourse_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppTag_ID, t.AppCourse_ID })
                .ForeignKey("dbo.AppTags", t => t.AppTag_ID, cascadeDelete: true)
                .ForeignKey("dbo.AppCourses", t => t.AppCourse_ID, cascadeDelete: true)
                .Index(t => t.AppTag_ID)
                .Index(t => t.AppCourse_ID);
            
            AlterColumn("dbo.AppCourses", "AppCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.AppCourses", "AppCategoryID");
            AddForeignKey("dbo.AppCourses", "AppCategoryID", "dbo.AppCategories", "ID", cascadeDelete: true);
            DropColumn("dbo.AppCourses", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppCourses", "CategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AppCourses", "AppCategoryID", "dbo.AppCategories");
            DropForeignKey("dbo.AppTagAppCourses", "AppCourse_ID", "dbo.AppCourses");
            DropForeignKey("dbo.AppTagAppCourses", "AppTag_ID", "dbo.AppTags");
            DropIndex("dbo.AppTagAppCourses", new[] { "AppCourse_ID" });
            DropIndex("dbo.AppTagAppCourses", new[] { "AppTag_ID" });
            DropIndex("dbo.AppCourses", new[] { "AppCategoryID" });
            AlterColumn("dbo.AppCourses", "AppCategoryID", c => c.Int());
            DropTable("dbo.AppTagAppCourses");
            RenameColumn(table: "dbo.AppCourses", name: "AppCategoryID", newName: "AppCategory_ID");
            CreateIndex("dbo.AppCourses", "AppCategory_ID");
            AddForeignKey("dbo.AppCourses", "AppCategory_ID", "dbo.AppCategories", "ID");
        }
    }
}
