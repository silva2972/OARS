using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(9, ErrorMessage = "Must be 9 characters long", MinimumLength = 9)]
        [Display(Name = "SSN")]
        public string ssn { get; set; }
        
        [Display(Name = "Staff Number")]
        public int? s_no { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DOB")]
        public DateTime dob { get; set; }
    }
}
