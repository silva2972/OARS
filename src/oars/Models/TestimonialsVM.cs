using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class TestimonialsVM
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime TestimonialDate { get; set; }
        [Display(Name = "Testimonial")]
        public string TestimonialContent { get; set; }
    }
}
