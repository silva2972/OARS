using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class RentalInvoice
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceDue { get; set; }
        public string CcNo { get; set; }
        public string CcType { get; set; }
        public DateTime CcExpDate { get; set; }
        public decimal CcAmt { get; set; }
        public int RentalNo { get; set; }

        public virtual Rental RentalNoNavigation { get; set; }
    }
}
