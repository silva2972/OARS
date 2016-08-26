using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oars.Models.DB;
using oars.Models;

namespace oars.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly OARSContext _context;

        public TestimonialsController(OARSContext context)
        {
            _context = context;    
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            var model = await _context.Testimonials.Select(t => new TestimonialsVM
            {
                TestimonialDate = t.TestimonialDate,
                TestimonialContent = t.TestimonialContent
            }).ToListAsync();
            return View(model);
        }
               
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string testimonial_text)
        {
            if (ModelState.IsValid)
            {
                Testimonials _testimonial = new Testimonials();
                _testimonial.TestimonialDate = DateTime.Today;
                _testimonial.TestimonialContent = testimonial_text;
                _testimonial.TenantSs = _context.Tenant.Single(t => t.Username == User.Identity.Name).TenantSs;
                _context.Add(_testimonial);
                await _context.SaveChangesAsync();                
            }
            return RedirectToAction("Index");
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials.SingleOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonials == null)
            {
                return NotFound();
            }
            ViewData["TenantSs"] = new SelectList(_context.Tenant, "TenantSs", "TenantSs", testimonials.TenantSs);
            return View(testimonials);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestimonialId,TenantSs,TestimonialContent,TestimonialDate")] Testimonials testimonials)
        {
            if (id != testimonials.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialsExists(testimonials.TestimonialId))
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
            ViewData["TenantSs"] = new SelectList(_context.Tenant, "TenantSs", "TenantSs", testimonials.TenantSs);
            return View(testimonials);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials.SingleOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonials == null)
            {
                return NotFound();
            }

            return View(testimonials);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonials = await _context.Testimonials.SingleOrDefaultAsync(m => m.TestimonialId == id);
            _context.Testimonials.Remove(testimonials);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TestimonialsExists(int id)
        {
            return _context.Testimonials.Any(e => e.TestimonialId == id);
        }
    }
}
