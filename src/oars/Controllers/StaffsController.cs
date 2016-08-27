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
using Microsoft.AspNetCore.Identity;

namespace oars.Controllers
{
    [Authorize(Policy = "Staff")]
    public class StaffsController : Controller
    {
        private readonly OARSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public StaffsController(UserManager<ApplicationUser> userManager, OARSContext context)
        {
            _userManager = userManager;
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
        [Authorize(Roles ="Supervisor")]
        public async Task<IActionResult> StaffRentalSummary()
        {
            var model = await _context.Staff.Select(s => new StaffRentalSummaryVM
            {
                fname = s.Fname,
                lname = s.Lname,
                username = s.Username,
                rental_nos = _context.Rental.Count(r => r.StaffNo == s.StaffNo)
            }).ToListAsync();
            for(var i =0; i< model.Count; i++)
            {
                var user = await _userManager.FindByEmailAsync(model[i].username);
                var pos = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model[i].username));
                model[i].position = pos[0];
            }
            return View(model);
        }
        public async Task<IActionResult> StaffApartmentDetails()
        {
            var model = await _context.Staff.Select(s => new StaffApartmentDetailsVM
            {
                fname = s.Fname,
                lname = s.Lname,
                username = s.Username,
                apartments = _context.Rental.Where(r=>r.StaffNo==s.StaffNo).Select(r=>r.AptNo).ToList()
            }).ToListAsync();
            for (var i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByEmailAsync(model[i].username);
                var pos = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model[i].username));
                model[i].position = pos[0];
            }
            return View(model);
        }
    }
}
