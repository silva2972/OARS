using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class LeaseLengthListVM
    {
        [Display(Name="Rental Number")]
        public int rental_no { get; set; }
        [Display(Name = "Apartment Number")]
        public int apt_no { get; set; }
        [Display(Name = "Lease Type")]
        public byte lease_type { get; set; }
    }
}
