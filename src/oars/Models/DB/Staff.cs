using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Staff
    {
        public Staff()
        {
            Rental = new HashSet<Rental>();
        }

        public int StaffNo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public decimal Salary { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Rental> Rental { get; set; }
    }
}
