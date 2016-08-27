using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class VacantApartmentsListVM
    {
        [Display(Name = "Apartment Number")]
        public int apt_no { get; set; }
        [Display(Name = "Apartment Type")]
        public byte apt_type { get; set; }
        [Display(Name = "Deposit")]
        public int deposit { get; set; }
        [Display(Name = "Rent")]
        public decimal rent { get; set; }  
    }
}
