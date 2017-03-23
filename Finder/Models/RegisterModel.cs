using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder.Models
{
    public class RegisterModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "You must enter a first name ")]
        [RegularExpression(@"[a-zA-Z \-]+", ErrorMessage = "First name can only contain letters")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "You must enter a last name")]
        [RegularExpression(@"[a-zA-Z \-]+", ErrorMessage = "Last name can only contain letters")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "You must enter a username")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9._]*$", ErrorMessage = "Username contains invalid characters")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must enter a password")]
        public string Password { get; set; }
        [Display(Name = "Confirm your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must be between 6-20 characters.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The given passwords do not match")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Your password can only contain characters and numbers")]
        public string PasswordCheck { get; set; }
        [Display(Name = "Enter your age")]
        [Required(ErrorMessage = "You must enter your age")]
        [RegularExpression(@"^(0?[1-9]|[1-9][0-9])$", ErrorMessage = "Your age must be between 1-99")]
        public int Age { get; set; }
        [Display(Name = "Select your gender")]
        public string Gender { get; set; }
        [Display(Name = "Are you interested in meeting men?")]
        [Required(ErrorMessage = "You must select an option")]
        public bool IntrestMen { get; set; }
        [Display(Name = "Are you interested in meeting women?")]
        [Required(ErrorMessage = "You must select an option")]
        public bool IntrestWomen { get; set; }
        [Display(Name = "Do you want your profile to be visible through searches on the page?")]
        public bool Searchable { get; set; }
        [Display(Name = "Information about you")]
        [Required(ErrorMessage = "You must write something about yourself")]
        public string Description { get; set; }
    }
}
