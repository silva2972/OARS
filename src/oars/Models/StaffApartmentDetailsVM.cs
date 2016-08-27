using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class StaffApartmentDetailsVM
    {
        [Display(Name ="First Name")]
        public string fname { get; set; }
        [Display(Name = "Last Name")]
        public string lname { get; set; }
        public string username { get; set; }
        [Display(Name = "Position")]
        public string position { get; set; }
        [Display(Name = "Apartments")]
        public IList<int> apartments { get; set; }
    }
}
