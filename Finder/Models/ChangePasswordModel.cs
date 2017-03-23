using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finder.Models
{
    public class ChangePasswordModel
    {
        [Display(Name = "Old password")]
        [Required(ErrorMessage = "You must enter your old password")]
        public string oldPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "You must enter a password")]
        public string newPassword { get; set; }

        [Display(Name = "Confirm your new password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must be between 6-20 characters.")]
        [System.ComponentModel.DataAnnotations.Compare("newPassword", ErrorMessage = "The given passwords do not match")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Your password can only contain characters and numbers")]
        public string newPasswordCheck { get; set; }
    }
}