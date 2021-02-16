using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ehpad.ORM;

namespace ehpad.WEB.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly Context _context = new Context();

        // GET: Vaccines
        public async Task<IActionResult> Index()
        {
            var context = _context.Vaccines.Include(v => v.Brand).Include(v => v.Drug);
            return View(await context.OrderBy(vaccine => vaccine.Drug.Name).ThenBy(vaccine => vaccine.Brand.Name).ToListAsync());
        }

        // GET: Vaccines/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines
                .Include(v => v.Brand)
                .Include(v => v.Drug)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["DrugId"] = new SelectList(_context.Drugs, "Id", "Name");
            return View();
        }

        // POST: Vaccines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lot,DrugId,BrandId")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", vaccine.BrandId);
            ViewData["DrugId"] = new SelectList(_context.Drugs, "Id", "Name", vaccine.DrugId);
            return View(vaccine);
        }

        // GET: Vaccines/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", vaccine.BrandId);
            ViewData["DrugId"] = new SelectList(_context.Drugs, "Id", "Name", vaccine.DrugId);
            return View(vaccine);
        }

        // POST: Vaccines/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lot,DrugId,BrandId")] Vaccine vaccine)
        {
            if (id != vaccine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", vaccine.BrandId);
            ViewData["DrugId"] = new SelectList(_context.Drugs, "Id", "Name", vaccine.DrugId);
            return View(vaccine);
        }

        // GET: Vaccines/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines
                .Include(v => v.Brand)
                .Include(v => v.Drug)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);
            _context.Vaccines.Remove(vaccine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
            return _context.Vaccines.Any(e => e.Id == id);
        }
    }
}
