namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coursecategorys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCourses", "CategoryID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCourses", "CategoryID");
        }
    }
}
