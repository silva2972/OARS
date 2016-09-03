using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class CreateRentalVM
    {
        [Required]
        [Display(Name = "SSN")]
        [StringLength(9,MinimumLength =9)]
        public string ssn { get; set; }
        [Required]
        [Display(Name = "Tenant Name")]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        [StringLength(1)]
        public string marital { get; set; }
        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        public string workPhone { get; set; }
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public string homePhone { get; set; }
        [Display(Name = "Employer")]
        public string employerName { get; set; }
        [Required]
        [Display(Name = "Gender")]
        [StringLength(1)]
        public string gender { get; set; }
        [Required]
        [Display(Name = "Apartment Number")]
        public int apt_no { get; set; }
        [Required]
        [Display(Name = "Apartment Type")]
        public byte apt_type { get; set; }
        [Required]
        [Display(Name = "Deposit")]
        [DataType(DataType.Currency)]
        public int deposit { get; set; }
        [Required]
        [Display(Name = "Lease Type")]
        public string lease_type { get; set; }
    }
}
