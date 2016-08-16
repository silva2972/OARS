using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oars.Models
{
    public class ReceiptViewModel
    {
        
        public string tenant_name { get; set; }
        public int rental_no { get; set; }
        public int apt_no { get; set; }
        public int  invoice_no { get; set; }
        [DataType(DataType.Date)]
        public DateTime invoide_date { get; set; }
        public decimal amt_paid { get; set; }
    }
}
