using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;

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
            var oARSContext = _context.Tenant.Include(t => t.RentalNoNavigation);
            return View(await oARSContext.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant.SingleOrDefaultAsync(m => m.TenantSs == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            ViewData["RentalNo"] = new SelectList(_context.Rental, "RentalNo", "RentalStatus");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantSs,EmployerName,Gender,HomePhone,Marital,RentalNo,TenantDob,TenantName,Username,WorkPhone")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RentalNo"] = new SelectList(_context.Rental, "RentalNo", "RentalStatus", tenant.RentalNo);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant.SingleOrDefaultAsync(m => m.TenantSs == id);
            if (tenant == null)
            {
                return NotFound();
            }
            ViewData["RentalNo"] = new SelectList(_context.Rental, "RentalNo", "RentalStatus", tenant.RentalNo);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenantSs,EmployerName,Gender,HomePhone,Marital,RentalNo,TenantDob,TenantName,Username,WorkPhone")] Tenant tenant)
        {
            if (id != tenant.TenantSs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantSs))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["RentalNo"] = new SelectList(_context.Rental, "RentalNo", "RentalStatus", tenant.RentalNo);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant.SingleOrDefaultAsync(m => m.TenantSs == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tenant = await _context.Tenant.SingleOrDefaultAsync(m => m.TenantSs == id);
            _context.Tenant.Remove(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TenantExists(string id)
        {
            return _context.Tenant.Any(e => e.TenantSs == id);
        }
    }
}
