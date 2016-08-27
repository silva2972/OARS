using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class RentCollectedVM
    {
        [Display(Name ="Year")]
        public int year { get; set; }
        [Display(Name = "Month")]
        public int month { get; set;}
        [Display(Name = "Rent Collected")]
        public decimal rent_collection { get; set; }
    }
}
