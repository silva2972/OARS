using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace oars.Models.DB
{
    public partial class RentalInvoice
    {
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public decimal InvoiceDue { get; set; }
        [Required]
        public string CcNo { get; set; }
        [Required]
        public string CcType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CcExpDate { get; set; }
        [Required]
        public decimal CcAmt { get; set; }
        [Required]
        public int RentalNo { get; set; }

        public virtual Rental RentalNoNavigation { get; set; }
    }
}
