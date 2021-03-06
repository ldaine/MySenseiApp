﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySensei.Models
{
    // Users
    public class ChangePassword
    {
        [Required(ErrorMessage = "Current Password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class CreateModel
    {
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }
        public string Zip { get; set; }
        public Cities City { get; set; }
        public string Country { get; set; }
        public string Avatar { get; set; }//link to image - maybe save the images in seperate table?
        public Gender Gender { get; set; } //male or female
        public string Biography { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        [Display(Name="Primary Language")]
        public Language PrimaryLanguage { get; set; }

    }
    public class UserEditModel
    {
        public List<string> Roles { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }
        public string Zip { get; set; }
        public Cities City { get; set; }
        public string Country { get; set; }
        public string Avatar { get; set; }//link to image - maybe save the images in seperate table?
        public Gender Gender { get; set; } //male or female
        public string Biography { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        [Display(Name = "Primary Language")]
        public Language PrimaryLanguage { get; set; }

    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }


    // Roles
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}