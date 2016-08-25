using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class ComplaintVM
    {
        [Display(Name ="Complaint Number")]
        public int ComplaintNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Complaint Date")]
        public DateTime ComplaintDate { get; set; }
        [Display(Name = "Rental Complaint")]
        public string RentalComplaint { get; set; }
        [Display(Name = "Apartment Complaint")]
        public string AptComplaint { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Rental Number")]
        public int? RentalNo { get; set; }
        [Display(Name = "Status")]
        public int? AptNo { get; set; }
    }
}
