using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensei.Models;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace MySensei.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("MySenseiDb") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public DbSet<AppCourse> Courses { get; set; }
        public DbSet<AppSignUp> SignUps { get; set; }
        public DbSet<AppTag> AppTags { get; set; }
        public DbSet<AppCategory> AppCategorys { get; set; }
        public DbSet<AppCourseStatus> AppCourseStatuss { get; set; }
        public IEnumerable AppUsers { get; internal set; }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}