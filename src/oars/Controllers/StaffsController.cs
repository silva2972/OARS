using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using oars.Models;

namespace oars.Controllers
{
    [Authorize(Policy = "Staff")]
    public class StaffsController : Controller
    {
        private readonly OARSContext _context;

        public StaffsController(OARSContext context)
        {
            _context = context;    
        }

        // GET: Staffs
        
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            var _staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);
            ViewBag.staffName = _staff.Fname + " " + _staff.Lname;
            return View();
        }

        public async Task<IActionResult> TenantList()
        {
            var model = await _context.Tenant.Select(t => new TenantList
            {
                rental_no = t.RentalNo,
                tenantName = t.TenantName,
                apt_no = _context.Rental.First(r => r.RentalNo == t.RentalNo).AptNo
            }).ToListAsync();
            return View(model);
        }

        public IActionResult PaymentList()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentList(int rental_no)
        {
             var model = await _context.RentalInvoice.Where(ri => ri.RentalNo == rental_no).Select(ri => new PaymentListVM
             {
                 rental_no = ri.RentalNo,
                 cc_amt = ri.CcAmt,
                 cc_type = ri.CcType,
                 invoice_date = ri.InvoiceDate,
                 invoice_no = ri.InvoiceNo
             }).ToListAsync();
            if (model.Any())
            {
                return View(model);
            }
            ViewBag.Error = "Invalid Rental Number";
            return View();
        }
    }
}
