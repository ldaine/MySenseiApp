using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySensei.Models
{
    public class AppUser: IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(10)]
        public string Zip { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(250)]
        public string Avatar { get; set; }//link to image - maybe save the images in seperate table?
        public double? Rating { get; set; }
        [StringLength(100)]
        public string Facebook { get; set; }
        [StringLength(100)]
        public string Google { get; set; }
        [StringLength(100)]
        public string Twitter { get; set; }
        [StringLength(10)]
        public string Gender { get; set; } //male or female
        [DataType(DataType.Text)]
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        [StringLength(50)]
        public string PrimaryLanguage { get; set; }
        public virtual ICollection<AppCourse> Courses { get; set; }
        public virtual ICollection<AppSignUp> SignUps { get; set; }
    }
}