using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class AppCourse
    {
        public int ID { get; set; }
        public string AppUserID { get; set; }
        public int AppCategoryID { get; set; }
        public int AppCourseStatusID { get; set; }
        public string Course { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string CourseImage { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int MaxAttendance { get; set; }


        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<AppSignUp> SignUps { get; set; }
        public virtual AppCategory AppCategory { get; set; }
        public virtual ICollection<AppTag> AppTags { get; set; }
        public virtual AppCourseStatus AppCourseStatus { get; set; }

    }
}