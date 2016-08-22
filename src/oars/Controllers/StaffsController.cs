using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace oars.Controllers
{
    public class StaffsController : Controller
    {
        private readonly OARSContext _context;

        public StaffsController(OARSContext context)
        {
            _context = context;    
        }

        // GET: Staffs
        [Authorize(Policy = "Staff")]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            var _staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);
            ViewBag.staffName = _staff.Fname + " " + _staff.Lname;
            return View();
        }


    }
}
