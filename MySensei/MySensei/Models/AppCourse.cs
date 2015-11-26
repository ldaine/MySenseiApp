﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class AppCourse
    {
        public int ID { get; set; }
        public string AppUserID { get; set; }
        public string Course { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<AppSignUp> SignUps { get; set; }

    }
}