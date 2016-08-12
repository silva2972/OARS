using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Tenant
    {
        public Tenant()
        {
            TenantAuto = new HashSet<TenantAuto>();
            TenantFamily = new HashSet<TenantFamily>();
            Testimonials = new HashSet<Testimonials>();
        }

        public string TenantSs { get; set; }
        public string TenantName { get; set; }
        public DateTime TenantDob { get; set; }
        public string Marital { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string EmployerName { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public int RentalNo { get; set; }

        public virtual ICollection<TenantAuto> TenantAuto { get; set; }
        public virtual ICollection<TenantFamily> TenantFamily { get; set; }
        public virtual ICollection<Testimonials> Testimonials { get; set; }
        public virtual Rental RentalNoNavigation { get; set; }
    }
}
