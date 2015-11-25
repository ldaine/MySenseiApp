using System.Collections.Generic;

namespace MySensei.Models
{
    public class AppCourse
    {
        public int AppCourseId { get; set; }
        public virtual ICollection<AppUser> Students { get; set; }
        public virtual AppUser Teacher { get; set; }

    }
}