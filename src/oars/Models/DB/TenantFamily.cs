using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class TenantFamily
    {
        public string FamilySs { get; set; }
        public string FullName { get; set; }
        public string Spouse { get; set; }
        public string Child { get; set; }
        public string Divorced { get; set; }
        public string Single { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string TenantSs { get; set; }

        public virtual Tenant TenantSsNavigation { get; set; }
    }
}
