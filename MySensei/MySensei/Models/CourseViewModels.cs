using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class CreateCourseModel
    {
        [Display(Name = "Teacher")]
        public string AppUserID { get; set; }
        [Display(Name = "Category")]
        public int AppCategoryID { get; set; }
        [Display(Name = "Status")]
        public int AppCourseStatusID { get; set; }
        [Display(Name = "Title")]
        public string Course { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        [Display(Name = "Path to Image")]
        public string CourseImage { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        [Display(Name = "Max Students")]
        public int MaxAttendance { get; set; }
    }
}