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
    public class InjectionsController : Controller
    {
        private readonly Context _context = new Context();

        // GET: Injections
        public async Task<IActionResult> Index()
        {
            var context = _context.Injections.Include(i => i.People).Include(i => i.Vaccine);
            return View(await context.ToListAsync());
        }

        // GET: Injections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .Include(i => i.People)
                .Include(i => i.Vaccine)
                .FirstOrDefaultAsync(m => m.PeopleId == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // GET: Injections/Create
        public IActionResult Create()
        {
            ViewData["PeopleId"] = new SelectList(_context.Peoples, "Id", "Condition");
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Lot");
            return View();
        }

        // POST: Injections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineDate,ReminderDate,VaccineId,PeopleId")] Injection injection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(injection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeopleId"] = new SelectList(_context.Peoples, "Id", "Condition", injection.PeopleId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Lot", injection.VaccineId);
            return View(injection);
        }

        // GET: Injections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }
            ViewData["PeopleId"] = new SelectList(_context.Peoples, "Id", "Condition", injection.PeopleId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Lot", injection.VaccineId);
            return View(injection);
        }

        // POST: Injections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineDate,ReminderDate,VaccineId,PeopleId")] Injection injection)
        {
            if (id != injection.PeopleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(injection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InjectionExists(injection.PeopleId))
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
            ViewData["PeopleId"] = new SelectList(_context.Peoples, "Id", "Condition", injection.PeopleId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Lot", injection.VaccineId);
            return View(injection);
        }

        // GET: Injections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .Include(i => i.People)
                .Include(i => i.Vaccine)
                .FirstOrDefaultAsync(m => m.PeopleId == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // POST: Injections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var injection = await _context.Injections.FindAsync(id);
            _context.Injections.Remove(injection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InjectionExists(int id)
        {
            return _context.Injections.Any(e => e.PeopleId == id);
        }
    }
}
