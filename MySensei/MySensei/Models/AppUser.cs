using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MySensei.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Avatar { get; set; }//link to image - maybe save the images in seperate table?
        public double? Rating { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
        public string Gender { get; set; } //male or female
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public string PrimaryLanguage { get; set; }
        public virtual ICollection<AppCourse> Courses { get; set; }
        public virtual ICollection<AppSignUp> SignUps { get; set; }
    }
}