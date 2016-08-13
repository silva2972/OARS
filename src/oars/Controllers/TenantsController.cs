using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;
using System.Dynamic;

namespace oars.Controllers
{
    public class TenantsController : Controller
    {
        private readonly OARSContext _context;

        public TenantsController(OARSContext context)
        {
            _context = context;    
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;            
            var _tenant = await _context.Tenant.FirstAsync(t => t.Username == username);

            ViewBag.tenantfamilylist = await _context.TenantFamily.Where(tf => tf.TenantSs == _tenant.TenantSs).ToListAsync();
            ViewBag.tenantautolist = await _context.TenantAuto.Where(ta => ta.TenantSs == _tenant.TenantSs).ToListAsync();
            ViewBag.tenantInfo = _tenant;

            return View();
        }

        public async Task<IActionResult> RentalAgreement()
        {
            var username = User.Identity.Name;
            var _tenant = await _context.Tenant.FirstAsync(t => t.Username == username);

            var rentalInfo = await _context.Rental.FirstAsync(r => r.RentalNo == _tenant.RentalNo);
            var apartmentInfo = await _context.Apartment.FirstAsync(a => a.AptNo == rentalInfo.AptNo);
            ViewBag.tenantInfo = _tenant;
            ViewBag.rentalInfo = rentalInfo;
            ViewBag.apartmentInfo = apartmentInfo;
            return View();
        }
        public async Task<IActionResult> PayRent()
        {
            var username = User.Identity.Name;
            var _tenant = await _context.Tenant.FirstAsync(t => t.Username == username);

            var rentalInfo = await _context.Rental.FirstAsync(r => r.RentalNo == _tenant.RentalNo);
            var apartmentInfo = await _context.Apartment.FirstAsync(a => a.AptNo == rentalInfo.AptNo);
            ViewBag.tenantInfo = _tenant;
            ViewBag.rentalInfo = rentalInfo;
            ViewBag.apartmentInfo = apartmentInfo;
            return View();
        }
    }
}
