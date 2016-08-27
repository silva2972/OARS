using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class AutomobileMakeCountVM
    {
        [Display(Name ="Automobile Make")]
        public string make { get; set; }
        [Display(Name = "Vehicle Count")]
        public int count { get; set; }
    }
}
