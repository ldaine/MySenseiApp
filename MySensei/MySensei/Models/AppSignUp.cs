using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class AppSignUp
    {
        public int ID { get; set; }
        public string AppUserID { get; set; }
        public int AppCourseID { get; set; }
        public DateTime? SignUpDate { get; set; }
        public int feedback { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual AppCourse AppCourse { get; set; }
    }
}