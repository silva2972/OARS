using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Rental
    {
        public Rental()
        {
            Complaints = new HashSet<Complaints>();
            RentalInvoice = new HashSet<RentalInvoice>();
            Tenant = new HashSet<Tenant>();
        }

        public int RentalNo { get; set; }
        public DateTime RentalDate { get; set; }
        public string RentalStatus { get; set; }
        public DateTime? CancelDate { get; set; }
        public byte LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }
        public DateTime? RenewalDate { get; set; }
        public int StaffNo { get; set; }
        public int AptNo { get; set; }

        public virtual ICollection<Complaints> Complaints { get; set; }
        public virtual ICollection<RentalInvoice> RentalInvoice { get; set; }
        public virtual ICollection<Tenant> Tenant { get; set; }
        public virtual Apartment AptNoNavigation { get; set; }
        public virtual Staff StaffNoNavigation { get; set; }
    }
}
