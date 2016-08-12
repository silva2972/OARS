using System;
using System.Collections.Generic;

namespace oars.Models.DB
{
    public partial class Testimonials
    {
        public int TestimonialId { get; set; }
        public DateTime TestimonialDate { get; set; }
        public string TestimonialContent { get; set; }
        public string TenantSs { get; set; }

        public virtual Tenant TenantSsNavigation { get; set; }
    }
}
