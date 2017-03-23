using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "You must enter a username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must enter a password")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}
