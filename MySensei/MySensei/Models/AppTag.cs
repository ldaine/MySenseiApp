using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class AppTag
    {
        public int ID { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<AppCourse> AppCourses { get; set; }
    }
}