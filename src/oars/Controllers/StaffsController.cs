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
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> StaffRentalSummary()
        {
            var model = await _context.Staff.Select(s => new StaffRentalSummaryVM
            {
                fname = s.Fname,
                lname = s.Lname,
                username = s.Username,
                rental_nos = _context.Rental.Count(r => r.StaffNo == s.StaffNo)
            }).ToListAsync();
            for (var i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByEmailAsync(model[i].username);
                var pos = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model[i].username));
                model[i].position = pos[0];
            }
            return View(model);
        }
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> StaffApartmentDetails()
        {
            var model = await _context.Staff.Select(s => new StaffApartmentDetailsVM
            {
                fname = s.Fname,
                lname = s.Lname,
                username = s.Username,
                apartments = _context.Rental.Where(r => r.StaffNo == s.StaffNo).Select(r => r.AptNo).ToList()
            }).ToListAsync();
            for (var i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByEmailAsync(model[i].username);
                var pos = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model[i].username));
                model[i].position = pos[0];
            }
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AutomobileMakeCount()
        {
            var model = await _context.TenantAuto.GroupBy(ta => new { ta.AutoMake })
                .Select(ta => new AutomobileMakeCountVM
                {
                    make = ta.Key.AutoMake,
                    count = ta.Count()
                }).ToListAsync();
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> VacantApartmentsList()
        {
            var model = await _context.Apartment.Where(a => a.AptStatus == "V").Select(a => new VacantApartmentsListVM
            {
                apt_no = a.AptNo,
                apt_type = a.AptType,
                deposit = a.AptDepositAmt,
                rent = a.AptRentAmt
            }).ToListAsync();
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LeaseLengthList()
        {
            var model = await _context.Rental.Select(r => new LeaseLengthListVM
            {
                rental_no = r.RentalNo,
                apt_no = r.AptNo,
                lease_type = r.LeaseType
            }).ToListAsync();
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RentCollected()
        {
            var model = await _context.RentalInvoice.GroupBy(ri => new
            {
                month = ri.InvoiceDate.Month,
                year = ri.InvoiceDate.Year,
            }).Select(ri => new RentCollectedVM {
                month = ri.Key.month,
                year = ri.Key.year,
                rent_collection = ri.Sum(_ri => _ri.CcAmt)
            }
            ).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> AvailableApartments()
        {
            var model = await _context.Apartment.Where(a => a.AptStatus == "V").Select(a => new VacantApartmentsListVM
            {
                apt_no = a.AptNo,
                apt_type = a.AptType,
                deposit = a.AptDepositAmt,
                rent = a.AptRentAmt
            }).ToListAsync();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AvailableApartments(int apt_no)
        {
            return RedirectToAction("CreateRental", new { aptNo = apt_no });
        }
        public async Task<IActionResult> CreateRental(int aptNo)
        {
            var _apt = await _context.Apartment.FirstAsync(a => a.AptNo == aptNo);
            if (_apt.AptStatus != "R")
            {
                ViewBag.apt_no = _apt.AptNo;
                ViewBag.apt_type = _apt.AptType;
                ViewBag.deposit = _apt.AptDepositAmt;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRental(CreateRentalVM model)
        {
            var _rental = new Rental();
            var _r = new Rental();
            var _tenant = new Tenant();
            var _staff = await _context.Staff.FirstAsync(s => s.Username == User.Identity.Name);
            _rental.RentalDate = DateTime.Today;
            if (model.lease_type.Equals("one"))
                _rental.LeaseType = 1;
            else
                _rental.LeaseType = 6;
            var tempDate = _rental.RentalDate.AddMonths(1);            
            _rental.LeaseStart = new DateTime(tempDate.Year, tempDate.Month, 1, 0, 0, 0, tempDate.Kind);        
            if (_rental.LeaseType == 1)
            {
                _rental.LeaseEnd = _rental.LeaseStart.AddYears(1).AddDays(-1);
                _rental.RenewalDate = _rental.LeaseStart.AddYears(1).AddMonths(-1).AddDays(-1);
            }
            else
            {
                _rental.LeaseEnd = _rental.LeaseStart.AddMonths(6).AddDays(-1);
                _rental.RenewalDate = _rental.LeaseStart.AddMonths(5).AddDays(-1);
            }
            _rental.RentalStatus = "S";
            _rental.CancelDate = _rental.LeaseStart.AddMonths(1);            
            _rental.AptNo = model.apt_no;
            _rental.StaffNo = _staff.StaffNo;            
            _context.Add(_rental);
            if (await _context.SaveChangesAsync() > 0)
            {
                _r = await _context.Rental.FirstAsync(r => r.AptNo == _rental.AptNo);
                if (_r.RentalNo > 0)
                {
                    _tenant.TenantSs = model.ssn;
                    _tenant.TenantName = model.name;
                    _tenant.TenantDob = model.dob;
                    _tenant.Gender = model.gender;
                    _tenant.Marital = model.marital;
                    _tenant.HomePhone = model.homePhone;
                    _tenant.EmployerName = model.employerName;
                    _tenant.WorkPhone = model.workPhone;
                    _tenant.RentalNo = _r.RentalNo;
                    _context.Add(_tenant);
                    var _apt = await _context.Apartment.SingleAsync(a => a.AptNo == _r.AptNo);
                    _apt.AptStatus = "R";
                    _context.Apartment.Update(_apt);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return RedirectToAction("RentalConfirmation", new { aptNo = model.apt_no });
                    }
                }
                return View(model);
            }                        
            return View(model);
            
        }
        public async Task<IActionResult> RentalConfirmation(int aptNo)
        {
            var model = new RentalConfirmationVM();
            if (aptNo > 0)
            {
                var _rental = await _context.Rental.FirstAsync(r => r.AptNo == aptNo);
                model.rentalNo = _rental.RentalNo;
                model.rentalDate = _rental.RentalDate;
                var _apt = await _context.Apartment.FirstAsync(a => a.AptNo == aptNo);
                model.rentalDeposit = _apt.AptDepositAmt;
                ViewBag.Message = "Passed";
            }
            else
                ViewBag.Message = aptNo;
            return View(model);
        }
    }
}
