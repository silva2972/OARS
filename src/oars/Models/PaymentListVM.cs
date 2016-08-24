using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class PaymentListVM
    {
        public int rental_no{get;set;}
        public int invoice_no{get;set;}
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime invoice_date{get;set;}
        public decimal cc_amt { get; set; }
        public string cc_type { get; set; }
    }
}
