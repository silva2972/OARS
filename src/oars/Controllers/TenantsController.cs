using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;
using System.Dynamic;
using oars.Models;
using Microsoft.AspNetCore.Authorization;

namespace oars.Controllers
{
    [Authorize(Roles = "Tenant")]
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
            ViewBag.RentalNo = rentalInfo.RentalNo;
            ViewBag.RentAmt = apartmentInfo.AptRentAmt;
            ViewBag.today = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayRent([Bind("InvoiceNo,CcAmt,CcExpDate,CcNo,CcType,InvoiceDate,InvoiceDue,RentalNo")] RentalInvoice rentalInvoice)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(rentalInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Receipt");
            }
            
            ViewData["RentalNo"] = new SelectList(_context.Rental, "RentalNo", "RentalStatus", rentalInvoice.RentalNo);
            return View(rentalInvoice);
        }
        public async Task<IActionResult> Receipt()
        {
            ReceiptViewModel rvm = new ReceiptViewModel();
            var username = User.Identity.Name;
            var _tenant = await _context.Tenant.FirstAsync(t => t.Username == username);
            var rentalInfo = await _context.Rental.FirstAsync(r => r.RentalNo == _tenant.RentalNo);
            var rentalInvoice = await _context.RentalInvoice.LastOrDefaultAsync(ri => ri.RentalNo == _tenant.RentalNo);
            rvm.rental_no = rentalInvoice.RentalNo;
            rvm.invoice_no = rentalInvoice.InvoiceNo;
            rvm.invoide_date = rentalInvoice.InvoiceDate;
            rvm.amt_paid = rentalInvoice.CcAmt;
            rvm.tenant_name = _tenant.TenantName;
            rvm.apt_no = rentalInfo.AptNo;
            return View(rvm);
        }
    }
}
