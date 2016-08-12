using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Complaints
    {
        public int ComplaintNo { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string RentalComplaint { get; set; }
        public string AptComplaint { get; set; }
        public string Status { get; set; }
        public int? RentalNo { get; set; }
        public int? AptNo { get; set; }

        public virtual Apartment AptNoNavigation { get; set; }
        public virtual Rental RentalNoNavigation { get; set; }
    }
}
