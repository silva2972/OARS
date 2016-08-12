using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Apartment
    {
        public Apartment()
        {
            Complaints = new HashSet<Complaints>();
            Rental = new HashSet<Rental>();
        }

        public int AptNo { get; set; }
        public byte AptType { get; set; }
        public string AptStatus { get; set; }
        public string AptUtility { get; set; }
        public int AptDepositAmt { get; set; }
        public decimal AptRentAmt { get; set; }

        public virtual ICollection<Complaints> Complaints { get; set; }
        public virtual ICollection<Rental> Rental { get; set; }
    }
}
