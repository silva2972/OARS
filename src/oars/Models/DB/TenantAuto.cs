using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class TenantAuto
    {
        public string LicenseNo { get; set; }
        public string AutoMake { get; set; }
        public string AutoModel { get; set; }
        public short AutoYear { get; set; }
        public string AutoColor { get; set; }
        public string TenantSs { get; set; }

        public virtual Tenant TenantSsNavigation { get; set; }
    }
}
