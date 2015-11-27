using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class AppCategory
    {
        public int ID { get; set; }
        public string Category { get; set; }

        public virtual ICollection<AppCourse> AppCourses { get; set; }
    }
}