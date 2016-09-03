using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class RentalConfirmationVM
    {
        [Display(Name = "Rental Number")]
        public int rentalNo { get; set; }
        [Display(Name = "Rental Date")]
        [DataType(DataType.Date)]
        public DateTime rentalDate { get; set; }
        [Display(Name = "Rental Deposit Amount")]
        [DataType(DataType.Currency)]
        public int rentalDeposit { get; set; }
        
    }
}
