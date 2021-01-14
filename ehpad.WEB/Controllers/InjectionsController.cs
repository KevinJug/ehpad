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
            var context = _context.Injections.Include(i => i.People).Include("Vaccine.Drug");
            return View(await context.OrderByDescending(injection => injection.ReminderDate).ThenBy(injection => injection.People.Name).ThenBy(injection => injection.People.Firstname).ToListAsync());
        }

        // GET: Injections/Details/5
        public async Task<IActionResult> Details(int? idVaccine, int? idPeople)
        {
            if (idVaccine == null || idPeople == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .Include(i => i.People)
                .Include("Vaccine.Drug")
                .FirstOrDefaultAsync(m => m.PeopleId == idPeople && m.VaccineId == idVaccine);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // GET: Injections/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> selectListPeople = from p in _context.Peoples
                                                     select new SelectListItem
                                                     {
                                                         Value = p.Id.ToString(),
                                                         Text = p.Name + " " + p.Firstname
                                                     };
            ViewData["PeopleId"] = new SelectList(
               selectListPeople.OrderBy(people => people.Text),
                "Value",
                "Text");

            IEnumerable<SelectListItem> selectListVaccin = from v in _context.Vaccines
                                                           select new SelectListItem
                                                           {
                                                               Value = v.Id.ToString(),
                                                               Text = v.Drug.Name + " (" + v.Lot + ")"
                                                           };
            ViewData["VaccineId"] = new SelectList(
               selectListVaccin.OrderBy(vaccin => vaccin.Text),
                "Value",
                "Text");

            //ViewData["PeopleId"] = new SelectList(_context.Peoples, "Id", "Condition");
            //ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Lot");
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
        public async Task<IActionResult> Edit(int? idVaccine, int? idPeople)
        {
            if (idVaccine == null || idPeople == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections.FirstOrDefaultAsync(m => m.PeopleId == idPeople && m.VaccineId == idVaccine); ;
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
        public async Task<IActionResult> Edit(int VaccineId, int PeopleId, [Bind("VaccineDate,ReminderDate,VaccineId,PeopleId")] Injection injection)
        {
            if (PeopleId != injection.PeopleId || VaccineId != injection.VaccineId)
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
                    if (!InjectionExists(injection.PeopleId, injection.VaccineId))
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
        public async Task<IActionResult> Delete(int? idVaccine, int? idPeople)
        {
            if (idVaccine == null || idPeople == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .Include(i => i.People)
                .Include("Vaccine.Drug")
                .FirstOrDefaultAsync(m => m.PeopleId == idPeople && m.VaccineId == idVaccine);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // POST: Injections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int VaccineId, int PeopleId)
        {
            var injection = await _context.Injections.FirstOrDefaultAsync(m => m.PeopleId == PeopleId && m.VaccineId == VaccineId);
            _context.Injections.Remove(injection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InjectionExists(int idPeople, int idVaccine)
        {
            return _context.Injections.Any(e => e.PeopleId == idPeople && e.VaccineId == idVaccine);
        }
    }
}
