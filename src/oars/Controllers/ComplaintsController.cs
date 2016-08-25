using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models;
using oars.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace oars.Controllers
{
    [Authorize(Roles = "Tenant,Supervisor,Manager,Customer-Service,Assistant")]
    public class ComplaintsController : Controller
    {
        private readonly OARSContext _context;

        public ComplaintsController(OARSContext context)
        {
            _context = context;    
        }

        // GET: Complaints
        [Authorize(Roles = "Supervisor")]               
        public async Task<IActionResult> Index()
        {
            var model = await _context.Complaints.Where(c => c.Status == "P").Select(c => new ComplaintVM
            {
                RentalComplaint = c.RentalComplaint,
                ComplaintDate = c.ComplaintDate,
                AptComplaint = c.AptComplaint,
                Status = c.Status,
                ComplaintNo = c.ComplaintNo,
                AptNo = c.AptNo,
                RentalNo = c.RentalNo
            }).ToListAsync();
            return View(model);
        }

        // GET: Complaints/Create
        [Authorize(Roles = "Tenant")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Tenant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string complaint_text)
        {
            var username = User.Identity.Name;
            var _tenant = await _context.Tenant.FirstAsync(t => t.Username == username);

            var rentalInfo = await _context.Rental.FirstAsync(r => r.RentalNo == _tenant.RentalNo);
            var _complaint = new Complaints();
            _complaint.RentalNo = rentalInfo.RentalNo;
            _complaint.AptNo = rentalInfo.AptNo;
            _complaint.AptComplaint = complaint_text;
            _complaint.ComplaintDate = DateTime.Today;
            _complaint.Status = "P";
            _context.Add(_complaint);
            if(await _context.SaveChangesAsync() > 0 )
                ViewBag.Message = "Complaint Submitted";
            else
                ViewBag.Message = "Complaint Submission Failed!";
            return View();
        }
        [Authorize(Policy = "Staff")]
        public IActionResult CreateRentalComplaint()
        {
            return View();
        }
        [Authorize(Policy = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRentalComplaint(string complaint_text,int r_no)
        {
            
            var rentalInfo = await _context.Rental.FirstAsync(r => r.RentalNo == r_no);
            var _complaint = new Complaints();
            _complaint.RentalComplaint = complaint_text;
            _complaint.RentalNo = r_no;
            _complaint.AptNo = _context.Rental.First(r => r.RentalNo == r_no).AptNo;
            _complaint.Status = "P";
            _complaint.ComplaintDate = DateTime.Today;
            _context.Add(_complaint);
            if (await _context.SaveChangesAsync() > 0)
                ViewBag.Message = "Complaint Submitted";
            else
                ViewBag.Message = "Complaint Submission Failed!";
            return View();
        }
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Update(int id)
        {
            var _complaint = await _context.Complaints.SingleOrDefaultAsync(c => c.ComplaintNo == id);
            _complaint.Status = "F";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
